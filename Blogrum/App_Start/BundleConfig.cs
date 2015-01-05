using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Blogrum
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Styles

            bundles.Add(new StyleBundle("~/content/css/styles").Include(
                "~/Content/css/normalize.css",
                "~/Content/css/bootstrap.css",
                "~/Content/css/style.css",
                "~/Content/css/theme.css"));

            #endregion

            #region Scripts

            bundles.Add(new ScriptBundle("~/scripts/modernizr").Include(
                "~/Scripts/modernizr-{version}.js"));

            bundles.Add(new ScriptBundle("~/Scripts/bundle").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/bootstrap.js",
                "~/Scripts/common.js"));

            #endregion
        }
    }
}