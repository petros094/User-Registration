using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_Project.Models.Entities;

namespace Task_Project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        private UsersEntity dbContext = new UsersEntity();

        public ActionResult Index()
        {
            var Login = System.Web.HttpContext.Current.User.Identity.Name;
            var user = dbContext.Users.SingleOrDefault(w => w.Login == Login);
            return View(user);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}