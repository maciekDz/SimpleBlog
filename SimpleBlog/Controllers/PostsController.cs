﻿using NHibernate.Linq;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class PostsController:Controller
    {
        private const int PostsPerPage = 5;

        public string Slugify { get; private set; }
        public ActionResult Index(int page = 1)
        {
            var baseQuery = Database.Session.Query<Post>().Where(p=>p.DeletedAt==null).OrderByDescending(f => f.CreatedAt);
            var totalPostsCount = baseQuery.Count();

            var postIds = baseQuery
                .Skip((page - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .Select(p => p.PostId)
                .ToArray();

            var currentPostPage = baseQuery
                .Where(p => postIds.Contains(p.PostId))
                .FetchMany(f => f.Tags)
                .Fetch(f => f.User)
                .ToList();

            return View(new PostsIndex
            {
                Posts = new PagedData<Post>(currentPostPage, totalPostsCount, page, PostsPerPage)
            });
        }

        public  ActionResult Show(string idAndSlug)
        {
            var parts = SeparateIdAndSlug(idAndSlug);
            if (parts == null)
                return HttpNotFound();

            var post = Database.Session.Load<Post>(parts.Item1);
            if(post==null || post.IsDeleted)
                return HttpNotFound();

            if (!post.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToRoutePermanent("Post", new { postId = parts.Item1, slug = post.Slug });

            return View(new PostsShow
            {
                Post = post
            });
        }

        public ActionResult Tag(string idAndSlug, int page=1)
        {
            var parts = SeparateIdAndSlug(idAndSlug);
            if (parts == null)
                return HttpNotFound();

            var tag = Database.Session.Load<Tag>(parts.Item1);
            if (tag == null )
                return HttpNotFound();

            if (!tag.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToRoutePermanent("Tag", new { tagId = parts.Item1, slug = tag.Slug });

            var totalPostCount = tag.Posts.Count();
            var postIds = tag.Posts.Skip((page - 1) * PostsPerPage)
                .OrderByDescending(p=>p.CreatedAt)
                .Take(PostsPerPage)
                .Where(t => t.DeletedAt == null)
                .Select(t => t.PostId)
                .ToArray();

            var posts = Database.Session.Query<Post>()
                .OrderByDescending(b => b.CreatedAt)
                .Where(t => postIds.Contains(t.PostId))
                .FetchMany(f => f.Tags)
                .Fetch(f => f.User)
                .ToList();

            return View(new PostTag
            {
                Tag = tag,
                Posts=new PagedData<Post>(posts,totalPostCount,page,PostsPerPage)
            });
        }

        private System.Tuple<int, string> SeparateIdAndSlug(string idAndSlug)
        {
            var matches = Regex.Match(idAndSlug, @"^(\d+)\-(.*)?$");
            if (!matches.Success)
                return null;

            var id = int.Parse(matches.Result("$1"));
            var slug = matches.Result("$2");

            return Tuple.Create(id, slug);
        }
    }
}