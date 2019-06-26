﻿using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTabAttribute("users")]
    public class UsersController : Controller
    {
        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(new UsersIndex
            {
                Users = Database.Session.Query<User>().ToList()
            });
        }

        public ActionResult New()
        {
            return View(new UsersNew { });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(UsersNew form)
        {
            if (Database.Session.Query<User>().Any(u => u.UserName == form.UserName))
                ModelState.AddModelError("UserName", "User Name must be unique");

            if (!ModelState.IsValid)
                return View(form);

            var user = new User
            {
                Email = form.Email,
                UserName = form.UserName
            };

            user.SetPassword(form.Password);
            Database.Session.Save(user);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int userId)
        {
            var user = Database.Session.Load<User>(userId);
            if (user == null)
                return HttpNotFound();

            return View(new UsersEdit
            {
                UserName = user.UserName,
                Email = user.Email
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int userId,UsersEdit form)
        {
            var user = Database.Session.Load<User>(userId);
            if (user == null)
                return HttpNotFound();

            if (Database.Session.Query<User>().Any(u => u.UserName == form.UserName && u.UserId!=userId))
                ModelState.AddModelError("UserName", "User Name must be unique");

            if (!ModelState.IsValid)
                return View(form);

            user.UserName = form.UserName;
            user.Email = form.Email;

            Database.Session.Update(user);

            return RedirectToAction("Index");
        }

        public ActionResult ResetPassword(int userId)
        {
            var user = Database.Session.Load<User>(userId);
            if (user == null)
                return HttpNotFound();

            return View(new UsersResetPassword
            {
                UserName = user.UserName
                
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(int userId,UsersResetPassword form)
        {
            var user = Database.Session.Load<User>(userId);
            if (user == null)
                return HttpNotFound();

            form.UserName = user.UserName;

            if (!ModelState.IsValid)
                return View(form);

            user.SetPassword(form.Password);

            Database.Session.Update(user);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int userId)
        {
            var user = Database.Session.Load<User>(userId);
            if (user == null)
                return HttpNotFound();

            Database.Session.Delete(user);

            return RedirectToAction("Index");
        }
    }
}