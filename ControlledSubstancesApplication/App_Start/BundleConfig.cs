using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ControlledSubstancesApplication
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
           "~/Scripts/bootstrap.js"
           //"~/Scripts/bootstrap-datepicker.js"
           ));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //"~/Scripts/jquery-{version}.js",
            "~/Scripts/jquery-1.10.2.min.js",
            "~/Scripts/jquery.unobtrusive-ajax.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                "~/Scripts/jquery-ui.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/ie8scripts").Include(
                "~/Scripts/html5shiv.js",
                "~/Scripts/respond.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            "~/Scripts/jquery.validate*",
            "~/Scripts/additional-methods.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/jquery-css").Include(
                "~/Content/jquery-ui.css",
                "~/Content/jquery-ui.structure.css",
                "~/Content/jquery-ui.theme.css"
                ));        

            bundles.Add(new StyleBundle("~/Content/css").Include(
            "~/Content/bootstrap.css",
            "~/Content/Site.css",
            "~/Content/style.css",
            "~/Content/data.css"


            ));
        }
    }
}