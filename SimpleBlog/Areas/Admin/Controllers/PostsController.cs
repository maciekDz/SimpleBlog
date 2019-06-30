using SimpleBlog.Areas.Admin.ViewModels;
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

        public ActionResult New()
        {
            return View("Form",new PostsForm
            {
                IsNew=true
            });
        }

        public ActionResult Edit(int postId)
        {
            var post = Database.Session.Load<Post>(postId);
            if (post == null)
                return HttpNotFound();

            return View("Form", new PostsForm
            {
                IsNew = false,
                PostId = post.PostId,
                Title = post.Title,
                Slug = post.Slug,
                Content = post.Content
            });
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = form.PostId == null;

            if (!ModelState.IsValid)
                return View(form);

            Post post;
            if (form.IsNew)
            {
                post = new Post
                {
                    CreatedAt = DateTime.UtcNow,
                    User = Auth.User
                };
            }
            else
            {
                post = Database.Session.Load<Post>(form.PostId);

                if (post == null)
                    return HttpNotFound();

                post.UpdatedAt = DateTime.UtcNow;
            }

            post.Title = form.Title;
            post.Slug = form.Slug;
            post.Content = form.Content;

            Database.Session.SaveOrUpdate(post);

            return RedirectToAction("Index");
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Trash(int postId)
        {
            var post = Database.Session.Load<Post>(postId);
            if (post == null)
                return HttpNotFound();

            post.DeletedAt = DateTime.UtcNow;
            Database.Session.Update(post);
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int postId)
        {
            var post = Database.Session.Load<Post>(postId);
            if (post == null)
                return HttpNotFound();

            Database.Session.Delete(post);
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Restore(int postId)
        {
            var post = Database.Session.Load<Post>(postId);
            if (post == null)
                return HttpNotFound();

            post.DeletedAt =null;
            Database.Session.Update(post);
            return RedirectToAction("Index");
        }
    }
}