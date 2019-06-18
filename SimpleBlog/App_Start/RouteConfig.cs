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
                name: "Login",
                url: "login",
                namespaces: myNamespaces,
                defaults: new { controller = "Auth", action = "Login"}
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
