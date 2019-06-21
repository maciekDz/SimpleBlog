using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AuthLoginViewModel form)
        {
            if (!ModelState.IsValid)
               return View(form);
            
            return Content("The Form is valid");
        }
    }
}