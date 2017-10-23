using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtual.Models;
using TiendaVirtual.ViewModel;

namespace TiendaVirtual.Controllers
{
    public class OrdersController : Controller
    {
        TiendaVirtualContext db = new TiendaVirtualContext();

        // GET: Orders
        public ActionResult NewOrder()
        {
            var OrderView = new OrderView();
            OrderView.Customer = new Customer();
            OrderView.Products = new List<ProductOrder>();
            Session["OrderView"] = OrderView;

            var lista = db.Customers.ToList();
            lista.Add(new Customer {CustomerID = 0, Name = "[Seleccione un Cliente...]"});
            lista = lista.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(lista, "CustomerID", "FullName");
            return View(OrderView);
        }

        [HttpPost]
        public ActionResult NewOrder(OrderView orderView)
        {
            orderView = Session["OrderView"] as OrderView;
            var customerId = int.Parse(Request["CustomerID"]);

            if (customerId == 0) {
                var list = db.Customers.ToList();
                list.Add(new Customer { CustomerID = 0, Name = "[Seleccione un Cliente...]" });
                list = list.OrderBy(c => c.FullName).ToList();
                ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName");
                ViewBag.Error = "Debe de seleccionar un Cliente.";
                return View(orderView);
            }

            var customer = db.Customers.Find(customerId);

            if (customer == null)
            {
                var lista2 = db.Customers.ToList();
                lista2.Add(new Customer { CustomerID = 0, Name = "[Seleccione un Cliente...]" });
                lista2 = lista2.OrderBy(c => c.FullName).ToList();
                ViewBag.CustomerID = new SelectList(lista2, "CustomerID", "FullName");
                ViewBag.Error = "Cliente inexistente.";
                return View(orderView);
            }

            if (orderView.Products.Count == 0) {
                var lista3 = db.Customers.ToList();
                lista3.Add(new Customer { CustomerID = 0, Name = "[Seleccione un Cliente...]" });
                lista3 = lista3.OrderBy(c => c.FullName).ToList();
                ViewBag.CustomerID = new SelectList(lista3, "CustomerID", "FullName");
                ViewBag.Error = "Debe de añadir productos a la orden.";
                return View();
            }

            int orderId = 0;

            using (var transaccion = db.Database.BeginTransaction()) {

                try
                {
                    var _order = new Order
                    {
                        CustomerID = customerId,
                        OrderDate = DateTime.Now,
                        OrderStatus = OrderStatus.Created
                    };

                    db.Orders.Add(_order);
                    db.SaveChanges();

                    orderId = db.Orders.ToList().Select(o => o.OrderID).Max();

                    foreach (var item in orderView.Products)
                    {
                        var orderDetail = new OrderDetails
                        {
                            ProductID = item.ProductID,
                            OrderID = orderId,
                            Description = item.Description,
                            Price = item.Price,
                            Quantity = item.Quantity
                        };
                        db.OrderDetails.Add(orderDetail);
                    }
                    db.SaveChanges();
                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    ViewBag.Error = "ERROR: " + ex.Message;
                    var lista4 = db.Customers.ToList();
                    lista4.Add(new Customer { CustomerID = 0, Name = "[Seleccione un Cliente...]" });
                    lista4 = lista4.OrderBy(c => c.FullName).ToList();
                    ViewBag.CustomerID = new SelectList(lista4, "CustomerID", "FullName");
                }

                
            }

            ViewBag.Message = string.Format("La orden {0} fue guardada con exito.",orderId);

            var lista = db.Customers.ToList();
            lista.Add(new Customer { CustomerID = 0, Name = "[Seleccione un Cliente...]" });
            lista = lista.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(lista, "CustomerID", "FullName");

            orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();
            Session["OrderView"] = orderView;

            return View(orderView);
        }

        public ActionResult AddProduct()
        {
            var lista = db.Products.ToList();
            lista.Add(new Product { ProductID = 0, Description = "[Seleccione un Producto...]" });
            lista = lista.OrderBy(c => c.Description).ToList();
            ViewBag.ProductID = new SelectList(lista, "ProductID", "Description");
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductOrder productOrder)
        {

            var orderView = Session["OrderView"] as OrderView;

            var productID = int.Parse(Request["ProductID"]);
            if (productID == 0) {
                var lista = db.Products.ToList();
                lista.Add(new Product { ProductID = 0, Description = "[Seleccione un Producto...]" });
                lista = lista.OrderBy(c => c.Description).ToList();
                ViewBag.ProductID = new SelectList(lista, "ProductID", "Description");
                 ViewBag.Error = "Debe Seleccionar un Producto.";
                return View(productOrder);
            }

            var product = db.Products.Find(productID);

            if (product == null) {
                var lista = db.Products.ToList();
                lista.Add(new Product { ProductID = 0, Description = "[Seleccione un Producto...]" });
                lista = lista.OrderBy(c => c.Description).ToList();
                ViewBag.ProductID = new SelectList(lista, "ProductID", "Description");
                ViewBag.Error = "El producto no existe.";
                return View(productOrder);
            }

            productOrder = orderView.Products.Find(p => p.ProductID == productID);

            if (productOrder == null)
            {
                productOrder = new ProductOrder
                {
                    Description = product.Description,
                    Price = product.Price,
                    ProductID = product.ProductID,
                    Quantity = float.Parse(Request["Quantity"])
                };
                orderView.Products.Add(productOrder);
            }
            else {
                productOrder.Quantity += float.Parse(Request["Quantity"]);
            }

            

            var listaC = db.Customers.ToList();
            listaC.Add(new Customer { CustomerID = 0, Name = "[Seleccione un Cliente...]" });
            listaC = listaC.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(listaC, "CustomerID", "FullName");


            return View("NewOrder",orderView);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}