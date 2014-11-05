using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLearn.Models;

namespace MVCLearn.Controllers
{
    public class HomeController : Controller
    {
        readonly List<Customer> customers = new List<Customer>();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GotoHome()
        {
            Customer newCustomer = new Customer(){Id = 1, Name = "first", Salary = 900.78};

            ViewData["CurrentDateTime"] = DateTime.Now;
            ViewData["NewCustomer"] = newCustomer;
            return View();
        }

        public ActionResult Customer()
        {
            Customer newCustomer = new Customer() { Id = 1, Name = "first", Salary = 900.78 };
            return View(newCustomer);
        }

        public ActionResult EnterCustomer()
        {
            return View(new Customer());
        }

        
        public ActionResult DisplayCustomer([Bind(Include = "Id,Name,Salary")] Customer customer1)
        {
            Customer customer = new Customer(){Id = Convert.ToInt32(Request.Form["Id"]), Name = Request.Form["Name"], Salary = Convert.ToDouble(Request.Form["Salary"])};
            return View(customer);
        }
    }
}