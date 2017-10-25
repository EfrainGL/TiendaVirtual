using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaVirtual.Controllers
{
    public class AJAXConceptController : Controller
    {
        // GET: AJAXConcept
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult JSONFactorial(int n) {
            if (!Request.IsAjaxRequest()) {
                return null;
            }

            var reuslt = new JsonResult
            {
                Data = new { Factorial = Factorial(n)}
            };

            return reuslt; 
        }

        private double Factorial(int n)
        {
            double factorial = 1;
            for (int i = 2; i <= n; i++)
            {
                factorial *= i;
            }
            return factorial;
        }
    }
}