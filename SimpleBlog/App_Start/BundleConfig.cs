using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SimpleBlog.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle ("~/admin/styles")
                .Include("~/Content/Styles/bootstrap.css")
                .Include("~/Content/Styles/Admin.css"));

            bundles.Add(new StyleBundle("~/styles")
                .Include("~/content/styles/bootstrap.css")
                .Include("~/content/styles/Site.css"));

            bundles.Add(new ScriptBundle("~/admin/scripts")
                .Include("~/scripts/bootstrap.js")
                .Include("~/scripts/jquery-3.4.1.js")
                .Include("~/scripts/jquery.validate.js")
                .Include("~/scripts/jquery.validate.unobtrusive.js")
                .Include("~/Areas/Admin/Scripts/Forms.js"));

            bundles.Add(new ScriptBundle("~/scripts")
                .Include("~/scripts/jquery-3.4.1.js")
                .Include("~/scripts/jquery.validate.js")
                .Include("~/scripts/jquery.validate.unobtrusive.js")
                .Include("~/scripts/bootstrap.js"));

        }
    }
}