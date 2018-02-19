using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Task_Project.Models.Entities;
using WebMatrix.WebData;

namespace Task_Project.Areas.Administrator.Controllers
{
    [Authorize(Users ="admin")]
    public class AdminController : Controller
    {
        // GET: Administrator/Admin
        
        private UsersEntity dbContext = new UsersEntity();

        public ActionResult Index(string Filter)
        {
            var users = dbContext.Users.ToList();
            if (Filter == "Gender")
                users = users.OrderBy(o => o.Gender).ToList();
            if (Filter == "Age")
                users = users.OrderBy(o => o.Age).ToList();
            if (Filter == "CreationDate")
                users = users.OrderBy(o => o.CreationDate).ToList();
            if (Filter == "Name")
                users = users.OrderBy(o => o.FirstName).ToList();
            if (Filter == "Block")
                users = users.Where(w => w.IsBlocked == true).ToList();
            if (Filter == "UnBlock")
                users = users.Where(w => w.IsBlocked == false).ToList();
            return View(users);
        }

        public ActionResult TriggerUser(int id)
        {
            try
            {
                var user = dbContext.Users.FirstOrDefault(f => f.UserId == id);
                if (user.IsBlocked)
                {
                    user.IsBlocked = false;
                }
                else
                {
                    user.IsBlocked = true;
                }
                dbContext.SaveChanges();
                return RedirectToAction("Index", "Administrator");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Administrator");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var user = dbContext.Users.Find(id);
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
                return RedirectToAction("Index", "Administrator");
            }
            catch
            {
                return RedirectToAction("Index", "Administrator");
            }
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