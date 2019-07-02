using SimpleBlog.Areas.Admin.Controllers;
using SimpleBlog.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var myNamespaces = new[] { typeof(Controllers.PostsController).Namespace };

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "PostForReal",
                url: "post/{idAndSlug}",
                namespaces: myNamespaces,
                defaults: new { controller = "Posts", action = "Show" }
            );

            routes.MapRoute(
                name: "Post",
                url: "post/{postId}-{slug}",
                namespaces: myNamespaces,
                defaults: new { controller = "Posts", action = "Show" }
            );

            routes.MapRoute(
                name: "TagsForReal",
                url: "tag/{idAndSlug}",
                namespaces: myNamespaces,
                defaults: new { controller = "Posts", action = "Tag" }
            );
            routes.MapRoute(
                name: "Tag",
                url: "tag/{tagId}-{slug}",
                namespaces: myNamespaces,
                defaults: new { controller = "Posts", action = "Tag" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                namespaces: myNamespaces,
                defaults: new { controller = "Auth", action = "Login"}
            );

            routes.MapRoute(
                name: "Logout",
                url: "logout",
                namespaces: myNamespaces,
                defaults: new { controller = "Auth", action = "Logout" }
            );

            routes.MapRoute(
                name: "Home",
                url: "",
                namespaces: myNamespaces,
                defaults: new { controller = "Posts", action = "Index" }
            );
        }
    }
}
