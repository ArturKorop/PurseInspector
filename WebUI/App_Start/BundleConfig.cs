﻿using System.Web;
using System.Web.Optimization;

namespace WebUI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.9.2.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/viewpursescripts").Include(
                "~/UserScripts/AddOperationScripts.js",
                "~/UserScripts/AutoCompleateScript.js",
                "~/UserScripts/ChangeOperationScripts.js",
                "~/UserScripts/DeleteOperationScripts.js",
                "~/UserScripts/MonthNavigationScripts.js",
                "~/UserScripts/SetButtonScripts.js",
                "~/UserScripts/ViewPurseScripts.js",
                "~/UserScripts/ViewYearsScripts.js"));

            bundles.Add(new ScriptBundle("~/bundles/diagram").Include(
                "~/Scripts/jquery.sparkline.js",
                "~/Scripts/jqPlot/jquery.jqplot.js",
                "~/Scripts/jqPlot/excanvas.js",
                "~/Scripts/jqPlot/Plugins/jqplot.pieRenderer.js",
                "~/Scripts/Flot/jquery.flot.js",
                "~/Scripts/Flot/jquery.flot.pie.js",
                "~/UserScripts/DiagramScripts.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/stylepurse").Include(
                "~/Content/StylePurse.css",
                "~/Scripts/jqPlot/jquery.jqplot.css"));

            bundles.Add(new StyleBundle("~/Content/styleviewyear").Include(
                "~/Content/StyleViewYear.css",
                "~/Scripts/jqPlot/jquery.jqplot.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery-ui-1.9.1.custom.css",
                        "~/Content/themes/base/jquery-ui-1.9.1.custom.min.css",
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}