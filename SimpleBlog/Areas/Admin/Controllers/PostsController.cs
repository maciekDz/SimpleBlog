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
    [Authorize(Roles ="admin")]
    [SelectedTabAttribute("posts")]
    public class PostsController : Controller
    {
        private const int PostsPerPage = 5;

        // GET: Admin/Posts
        public ActionResult Index(int page=1)
        {
            var totalPostsCount = Database.Session.Query<Post>().Count();

            var currentPostPage = Database.Session.Query<Post>()
                .OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .ToList();

            return View(new PostsIndex
            {
                Posts=new PagedData<Post>(currentPostPage,totalPostsCount,page,PostsPerPage)
            });
        }
    }
}