using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using SimpleBlog.Models;

namespace SimpleBlog.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Home");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AuthLoginViewModel form,string returnUrl)
        {

            var user = Database.Session.Query<User>().FirstOrDefault(u=>u.UserName==form.UserName);
            if (user == null)
                SimpleBlog.Models.User.FakeHash();//prevent the "time attack" - if user is null then normaly there is no password to be hashed against and the request time is significantly lower. that makes hackers aware if a given username (often email) is registered on a given website. so we simply hash an empty string to prolong the request time
            if (user==null || !user.CheckPassword(form.Password))
                ModelState.AddModelError("UserName", "User Name or password is incorrect");

            if (!ModelState.IsValid)
               return View(form);

            FormsAuthentication.SetAuthCookie(user.UserName, true);

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return RedirectToRoute("Home");
        }
    }
}