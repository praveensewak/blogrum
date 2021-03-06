﻿using System;
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

            bundles.Add(new StyleBundle("~/content/css/admin").Include(
                "~/Content/css/normalize.css",
                "~/Content/css/bootplus.css",
                "~/Content/css/bootplus-responsive.css",
                "~/Content/css/font-awesome.css",
                "~/Content/libs/sweet-alert/sweet-alert.css",
                "~/Content/libs/ghost/dropzone.css",
                "~/Content/libs/ghost/editor.css",
                "~/Content/css/admin.css"));

            #endregion

            #region Scripts

            bundles.Add(new ScriptBundle("~/scripts/modernizr").Include(
                "~/Scripts/modernizr-{version}.js"));

            bundles.Add(new ScriptBundle("~/scripts/common").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/bootstrap.js",
                "~/Scripts/common.js"));

            bundles.Add(new ScriptBundle("~/scripts/admin").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/bootstrap.js",
                "~/Scripts/libs/sweet-alert/sweet-alert.js",
                "~/Scripts/libs/ghost/dropzone.js",
                "~/Scripts/libs/ghost/ghostdown.js",
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.sortable.js",
                "~/Scripts/knockout.mapping.js",
                "~/Scripts/knockout.helpers.js",
                "~/Scripts/knockout.lazyload.js",
                "~/Scripts/knockout.contextmenu.js"));

            #endregion
        }
    }
}