using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Task_Project.Models;
using Task_Project.Models.Entities;
using Facebook;
using Newtonsoft.Json;
using Microsoft.Web.WebPages.OAuth;
using Task_Project.Areas.Administrator.Models;

namespace Task_Project.Controllers
{
    public class AccountController : Controller
    {
        private UsersEntity dbContext = new UsersEntity();
        // GET: Account
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login");

        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserRegistration(User user)
        {
            try
            {
                user.CreationDate = DateTime.Now;
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                return View("Login");
            }
            catch
            {
                return HttpNotFound();
            }

        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            UserAdmin useradmin = new UserAdmin() { Login = model.Login, Password = model.Password };
            if (Repository.GetUserDetails(useradmin))
            {
                FormsAuthentication.SetAuthCookie(useradmin.Login, true);
                return RedirectToAction("Index","Administrator");
            }
            
            var user = dbContext.Users.SingleOrDefault(w => w.Login == model.Login && w.Password == model.Password);
            if (user == null)
            {
                ViewBag.text = "Invalid Login/Password";
                return View();
            }
            if (user.IsBlocked)
            {
                ViewBag.text = "User is blocked";
                return View();

            }
            FormsAuthentication.SetAuthCookie(user.Login, true);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }


        public void ExternalLogin(string provider)
        {

            OAuthWebSecurity.RequestAuthentication(provider, Url.Action("ExternalLoginCallback"));
        }
        public ActionResult ExternalLoginCallback()
        {
            var result = OAuthWebSecurity.VerifyAuthentication();

            if (result.IsSuccessful == false)
            {
                return RedirectToAction("Error", "Account");
            }

            FormsAuthentication.SetAuthCookie(result.UserName, false);

            return Redirect(Url.Action("Index", "Home"));
        }

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "2052163195061684",
                client_secret = "6560e54310a4941f44cc03d66b2de0db",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "2052163195061684",
                client_secret = "6560e54310a4941f44cc03d66b2de0db",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;

            Session["AccessToken"] = accessToken;

            fb.AccessToken = accessToken;

            dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
            string email = me.email;
            string firstname = me.first_name;
            string middlename = me.middle_name;
            string lastname = me.last_name;

            FormsAuthentication.SetAuthCookie(email, false);
            return RedirectToAction("Index", "Home");
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