using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaVirtual.Models;
using TiendaVirtual.ViewModel;

namespace TiendaVirtual.Controllers
{
    
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Users
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = db.Users.ToList();
            var usersView = new List<UserView>();

            foreach (var user in users)
            {
                var userView = new UserView {
                    Name = user.UserName,
                    userId = user.Id,
                };

                usersView.Add(userView);
            }
            return View(usersView);
        }

        public ActionResult Roles(string UserId) {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = db.Roles.ToList();
            var users = db.Users.ToList();
            var user =users.Find(e => e.Id == UserId);

            var rolesView = new List<RoleView>();
             
            foreach (var item in user.Roles)
            {
                var role = roles.Find(r => r.Id == item.RoleId);

                var roleView = new RoleView {
                    RoleId = role.Id,
                    Name = role.Name
                };

                rolesView.Add(roleView);
            }
            

            var userView = new UserView {
                Name = user.UserName,
                userId = user.Id,
                Roles = rolesView
            };

            return View(userView);
        }

        //GET
        public ActionResult AddRole(string userID) {

            if (string.IsNullOrEmpty(userID)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = db.Users.ToList();
            var user = users.Find(e => e.Id == userID);

            if (user == null) {
                return HttpNotFound();
            }
     
            var userView = new UserView
            {
                Name = user.UserName,
                userId = user.Id,
            };
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var lista = db.Roles.ToList();
            lista.Add(new IdentityRole { Id = "", Name = "[Seleccione un Role...]" });
            lista = lista.OrderBy(r => r.Name).ToList();
            ViewBag.RoleID = new SelectList(lista, "Id", "Name");

            return View(userView);
        }

        //POST
        [HttpPost]
        public ActionResult AddRole(string userID, FormCollection form)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = db.Users.ToList();
            var user = users.Find(e => e.Id == userID);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userView = new UserView
            {
                Name = user.UserName,
                userId = user.Id,
            };
            var roleId = Request["RoleID"];

            if (string.IsNullOrEmpty(roleId)) {
                ViewBag.Message = "Debe de seleccionar un Role";
                var lista = db.Roles.ToList();
                lista.Add(new IdentityRole { Id = "", Name = "[Seleccione un Role...]" });
                lista = lista.OrderBy(r => r.Name).ToList();
                ViewBag.RoleID = new SelectList(lista, "Id", "Name");
                return View(userView);
            }

            var rol = db.Roles.ToList().Find(r => r.Id == roleId);
            var roles = db.Roles.ToList();
            if (!userManager.IsInRole(user.Id, rol.Name)) {
                userManager.AddToRole(user.Id, rol.Name);
            }

            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                var role = roles.Find(r => r.Id == item.RoleId);

                var roleView = new RoleView
                {
                    RoleId = role.Id,
                    Name = role.Name
                };

                rolesView.Add(roleView);
            }


             userView = new UserView
            {
                Name = user.UserName,
                userId = user.Id,
                Roles = rolesView
            };

            return View("Roles", userView);
        }

        public ActionResult Delete(string userID, string RoleID) {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(RoleID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
           
            var user = db.Users.ToList().Find(u => u.Id == userID);
            var role = db.Roles.ToList().Find(r => r.Id == RoleID);
            var roles = db.Roles.ToList();

            if (userManager.IsInRole(user.Id,role.Name)) {
                userManager.RemoveFromRole(user.Id,role.Name);
            }
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);

                var roleView = new RoleView
                {
                    RoleId = role.Id,
                    Name = role.Name
                };

                rolesView.Add(roleView);
            }


           var userView = new UserView
            {
                Name = user.UserName,
                userId = user.Id,
                Roles = rolesView
            };
            return View("Roles", userView);

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