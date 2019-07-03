using SimpleBlog.Models;
using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class LayoutController : Controller
    {
        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            return View(new LayoutSidebar
            {
                IsLoggedIn = Auth.User != null,
                UserName = Auth.User != null ? Auth.User.UserName : "",
                IsAdmin = User.IsInRole("Admin"),
                Tags = Database.Session.Query<Tag>().Select(tag => new
                {
                    tag.TagId,
                    tag.Name,
                    tag.Slug,
                    PostCount = tag.Posts.Count
                }).Where(t => t.PostCount> 0).OrderByDescending(c => c.PostCount).Select(
                    tag => new SidebarTag(tag.TagId, tag.Name, tag.Slug, tag.PostCount)).ToList()
            });
        }
    }
}