using System.Web;
using System.Web.Optimization;

namespace InternalModule.Boilerplate
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var byFeature = true;
            if (byFeature)
            {
                bundles.Add(new ScriptBundle("~/bundles/ng-script").Include(
                    "~/app/module/inspinia.js",
                    "~/app/module/app.js",
                    "~/app/module/route.config.js",
                    "~/app/module/translations.js",
                    "~/app/module/directives.js",

                    "~/app/module/layout/layout.module.js",
                    "~/app/module/dashboard/dashboard.module.js",
                    "~/app/module/chart/chart.module.js",
                    "~/app/module/app_view/view.module.js",
                    "~/app/module/form/form.module.js",
                    "~/app/module/metric/metric.module.js",
                    "~/app/module/miscellaneous/miscellaneous.module.js",
                    "~/app/module/table/table.module.js",
                    "~/app/module/ui/ui.module.js",
                    "~/app/module/urgent_form/jobs.module.js",
                    "~/app/module/core/core.module.js",
                    "~/app/module/login/login.module.js",
                    "~/app/module/user/user.module.js",

                    "~/app/module/core/jobService.js",
                    "~/app/module/core/carService.js",
                    "~/app/module/core/priceDetailService.js",
                    "~/app/module/core/dropdownService.js",
                    "~/app/module/core/insuranceService.js",
                    "~/app/module/core/reportService.js",
                    "~/app/module/core/userService.js",

                    "~/app/module/core/directives/customSelect.js",
                    "~/app/module/layout/MainCtrl.js",
                    "~/app/module/urgent_form/jobs.js",
                    "~/app/module/layout/layout.js",
                    "~/app/module/login/login.js",
                    "~/app/module/user/user.js",
                    "~/app/module/dashboard/dashboard.js",
                    "~/app/module/job/container.js",
                    "~/app/module/insurance/insurance.js",
                    "~/app/module/dropdown/insurance/insurance.js",
                    "~/app/module/dropdown/brand/brand.js",
                    "~/app/module/dropdown/model/model.js",
                    "~/app/module/dropdown/comment/comment.js",
                    "~/app/module/dropdown/detailRepair/detailRepair.js",
                    "~/app/module/job/optionform.js",
                    "~/app/module/job/repairform.js",
                    "~/app/module/job/costrepairform.js",
                    "~/app/module/job/car/car.js",
                    "~/app/module/job/jobhistory.js",
                    "~/app/module/job/jobimage.js",
                    "~/app/module/report/job/job.js",
                    "~/app/module/report/tradeIn/tradeIn.js",
                    "~/app/module/profile/index.js",
                    "~/app/module/layout/translateCtrl.js",
                    "~/app/module/app_view/CalendarCtrl.js",
                    "~/app/module/chart/chartistCtrl.js",
                    "~/app/module/chart/chartJsCtrl.js",
                    "~/app/module/chart/flotChartCtrl.js",
                    "~/app/module/chart/rickshawChartCtrl.js",
                    "~/app/module/chart/sparklineChartCtrl.js",
                    "~/app/module/chart/widgetFlotChart.js",
                    "~/app/module/dashboard/dashboardFlotOne.js",
                    "~/app/module/dashboard/dashboardFlotTwo.js",
                    "~/app/module/dashboard/dashboardFive.js",
                    "~/app/module/dashboard/dashboardMap.js",
                    "~/app/module/form/imageCrop.js",
                    "~/app/module/form/ionSlider.js",
                    "~/app/module/form/selectCtrl.js",
                    "~/app/module/form/wizardCtrl.js",
                    "~/app/module/metric/metricsCtrl.js",
                    "~/app/module/miscellaneous/agileBoard.js",
                    "~/app/module/miscellaneous/codeEditorCtrl.js",
                    "~/app/module/miscellaneous/diff.js",
                    "~/app/module/miscellaneous/formValidation.js",
                    "~/app/module/miscellaneous/GoogleMaps.js",
                    "~/app/module/miscellaneous/idleTimer.js",
                    "~/app/module/miscellaneous/liveFavicon.js",
                    "~/app/module/miscellaneous/modalDemoCtrl.js",
                    "~/app/module/miscellaneous/ModalInstanceCtrl.js",
                    "~/app/module/miscellaneous/nestableCtrl.js",
                    "~/app/module/miscellaneous/notifyCtrl.js",
                    "~/app/module/miscellaneous/sweetAlertCtrl.js",
                    "~/app/module/miscellaneous/toastrCtrl.js",
                    "~/app/module/table/ngGridCtrl.js",
                    "~/app/module/ui/draggablePanels.js"
                ));
            }
            else
            {
                bundles.Add(new ScriptBundle("~/bundles/ng-script").Include(
                    "~/app/component/inspinia.js",
                    "~/app/component/app.js",
                    "~/app/component/config.js",
                    "~/app/component/translations.js",
                    "~/app/component/directives.js",
                    "~/app/component/controllers.js"
                ));
            }

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/js/angular/angular.min.js",
                "~/js/angular/angular-sanitize.js",
                "~/js/plugins/oclazyload/dist/ocLazyLoad.min.js",
                "~/js/angular-translate/angular-translate.min.js",
                "~/js/ui-router/angular-ui-router.min.js",
                "~/js/bootstrap/ui-bootstrap-tpls-0.12.0.min.js",
                "~/js/plugins/angular-idle/angular-idle.js",
                "~/js/less/less.min.js"
              ));

            bundles.Add(new LessBundle("~/Content/less").Include(
                 "~/Content/less/style.less"));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // jQueryUI CSS
            bundles.Add(new ScriptBundle("~/Scripts/plugins/jquery-ui/jqueryuiStyles").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.css"));

            // jQueryUI 
            bundles.Add(new StyleBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/animate.css",
                      "~/Content/style.css"));


            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/Scripts/knockout-{version}.js",
                      "~/Scripts/app.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                      "~/js/plugins/metisMenu/jquery.metisMenu.js",
                      "~/js/plugins/pace/pace.min.js",
                      "~/js/inspinia.js"));

            // Inspinia skin config script
            bundles.Add(new ScriptBundle("~/bundles/skinConfig").Include(
                      "~/Scripts/app/skin.config.min.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Scripts/plugins/slimscroll/jquery.slimscroll.min.js"));

            // Peity
            bundles.Add(new ScriptBundle("~/plugins/peity").Include(
                      "~/Scripts/plugins/peity/jquery.peity.min.js"));

            // Video responsible
            bundles.Add(new ScriptBundle("~/plugins/videoResponsible").Include(
                      "~/Scripts/plugins/video/responsible-video.js"));

            // Lightbox gallery css styles
            bundles.Add(new StyleBundle("~/Content/plugins/blueimp/css/").Include(
                      "~/Content/plugins/blueimp/css/blueimp-gallery.min.css"));

            // Lightbox gallery
            bundles.Add(new ScriptBundle("~/plugins/lightboxGallery").Include(
                      "~/Scripts/plugins/blueimp/jquery.blueimp-gallery.min.js"));

            // Sparkline
            bundles.Add(new ScriptBundle("~/plugins/sparkline").Include(
                      "~/Scripts/plugins/sparkline/jquery.sparkline.min.js"));

            // Morriss chart css styles
            bundles.Add(new StyleBundle("~/plugins/morrisStyles").Include(
                      "~/Content/plugins/morris/morris-0.4.3.min.css"));

            // Morriss chart
            bundles.Add(new ScriptBundle("~/plugins/morris").Include(
                      "~/Scripts/plugins/morris/raphael-2.1.0.min.js",
                      "~/Scripts/plugins/morris/morris.js"));

            // Flot chart
            bundles.Add(new ScriptBundle("~/plugins/flot").Include(
                      "~/Scripts/plugins/flot/jquery.flot.js",
                      "~/Scripts/plugins/flot/jquery.flot.tooltip.min.js",
                      "~/Scripts/plugins/flot/jquery.flot.resize.js",
                      "~/Scripts/plugins/flot/jquery.flot.pie.js",
                      "~/Scripts/plugins/flot/jquery.flot.time.js",
                      "~/Scripts/plugins/flot/jquery.flot.spline.js"));

            // Rickshaw chart
            bundles.Add(new ScriptBundle("~/plugins/rickshaw").Include(
                      "~/Scripts/plugins/rickshaw/vendor/d3.v3.js",
                      "~/Scripts/plugins/rickshaw/rickshaw.min.js"));

            // ChartJS chart
            bundles.Add(new ScriptBundle("~/plugins/chartJs").Include(
                      "~/Scripts/plugins/chartjs/Chart.min.js"));

            // iCheck css styles
            bundles.Add(new StyleBundle("~/Content/plugins/iCheck/iCheckStyles").Include(
                      "~/Content/plugins/iCheck/custom.css"));

            // iCheck
            bundles.Add(new ScriptBundle("~/plugins/iCheck").Include(
                      "~/Scripts/plugins/iCheck/icheck.min.js"));

            // dataTables css styles
            bundles.Add(new StyleBundle("~/Content/plugins/dataTables/dataTablesStyles").Include(
                      "~/Content/plugins/dataTables/dataTables.bootstrap.css",
                      "~/Content/plugins/dataTables/dataTables.responsive.css",
                      "~/Content/plugins/dataTables/dataTables.tableTools.min.css"));

            // dataTables 
            bundles.Add(new ScriptBundle("~/plugins/dataTables").Include(
                      "~/Scripts/plugins/dataTables/jquery.dataTables.js",
                      "~/Scripts/plugins/dataTables/dataTables.bootstrap.js",
                      "~/Scripts/plugins/dataTables/dataTables.responsive.js",
                      "~/Scripts/plugins/dataTables/dataTables.tableTools.min.js"));

            // jeditable 
            bundles.Add(new ScriptBundle("~/plugins/jeditable").Include(
                      "~/Scripts/plugins/jeditable/jquery.jeditable.js"));

            // jqGrid styles
            bundles.Add(new StyleBundle("~/Content/plugins/jqGrid/jqGridStyles").Include(
                      "~/Content/plugins/jqGrid/ui.jqgrid.css"));

            // jqGrid 
            bundles.Add(new ScriptBundle("~/plugins/jqGrid").Include(
                      "~/Scripts/plugins/jqGrid/i18n/grid.locale-en.js",
                      "~/Scripts/plugins/jqGrid/jquery.jqGrid.min.js"));

            // codeEditor styles
            bundles.Add(new StyleBundle("~/plugins/codeEditorStyles").Include(
                      "~/Content/plugins/codemirror/codemirror.css",
                      "~/Content/plugins/codemirror/ambiance.css"));

            // codeEditor 
            bundles.Add(new ScriptBundle("~/plugins/codeEditor").Include(
                      "~/Scripts/plugins/codemirror/codemirror.js",
                      "~/Scripts/plugins/codemirror/mode/javascript/javascript.js"));

            // codeEditor 
            bundles.Add(new ScriptBundle("~/plugins/nestable").Include(
                      "~/Scripts/plugins/nestable/jquery.nestable.js"));

            // validate 
            bundles.Add(new ScriptBundle("~/plugins/validate").Include(
                      "~/Scripts/plugins/validate/jquery.validate.min.js"));

            // fullCalendar styles
            bundles.Add(new StyleBundle("~/plugins/fullCalendarStyles").Include(
                      "~/Content/plugins/fullcalendar/fullcalendar.css"));

            // fullCalendar 
            bundles.Add(new ScriptBundle("~/plugins/fullCalendar").Include(
                      "~/Scripts/plugins/fullcalendar/moment.min.js",
                      "~/Scripts/plugins/fullcalendar/fullcalendar.min.js"));

            // vectorMap 
            bundles.Add(new ScriptBundle("~/plugins/vectorMap").Include(
                      "~/Scripts/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                      "~/Scripts/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"));

            // ionRange styles
            bundles.Add(new StyleBundle("~/Content/plugins/ionRangeSlider/ionRangeStyles").Include(
                      "~/Content/plugins/ionRangeSlider/ion.rangeSlider.css",
                      "~/Content/plugins/ionRangeSlider/ion.rangeSlider.skinFlat.css"));

            // ionRange 
            bundles.Add(new ScriptBundle("~/plugins/ionRange").Include(
                      "~/Scripts/plugins/ionRangeSlider/ion.rangeSlider.min.js"));

            // dataPicker styles
            bundles.Add(new StyleBundle("~/plugins/dataPickerStyles").Include(
                      "~/Content/plugins/datapicker/datepicker3.css"));

            // dataPicker 
            bundles.Add(new ScriptBundle("~/plugins/dataPicker").Include(
                      "~/Scripts/plugins/datapicker/bootstrap-datepicker.js"));

            // nouiSlider styles
            bundles.Add(new StyleBundle("~/plugins/nouiSliderStyles").Include(
                      "~/Content/plugins/nouslider/jquery.nouislider.css"));

            // nouiSlider 
            bundles.Add(new ScriptBundle("~/plugins/nouiSlider").Include(
                      "~/Scripts/plugins/nouslider/jquery.nouislider.min.js"));

            // jasnyBootstrap styles
            bundles.Add(new StyleBundle("~/plugins/jasnyBootstrapStyles").Include(
                      "~/Content/plugins/jasny/jasny-bootstrap.min.css"));

            // jasnyBootstrap 
            bundles.Add(new ScriptBundle("~/plugins/jasnyBootstrap").Include(
                      "~/Scripts/plugins/jasny/jasny-bootstrap.min.js"));

            // switchery styles
            bundles.Add(new StyleBundle("~/plugins/switcheryStyles").Include(
                      "~/Content/plugins/switchery/switchery.css"));

            // switchery 
            bundles.Add(new ScriptBundle("~/plugins/switchery").Include(
                      "~/Scripts/plugins/switchery/switchery.js"));

            // chosen styles
            bundles.Add(new StyleBundle("~/Content/plugins/chosen/chosenStyles").Include(
                      "~/Content/plugins/chosen/chosen.css"));

            // chosen 
            bundles.Add(new ScriptBundle("~/plugins/chosen").Include(
                      "~/Scripts/plugins/chosen/chosen.jquery.js"));

            // knob 
            bundles.Add(new ScriptBundle("~/plugins/knob").Include(
                      "~/Scripts/plugins/jsKnob/jquery.knob.js"));

            // wizardSteps styles
            bundles.Add(new StyleBundle("~/plugins/wizardStepsStyles").Include(
                      "~/Content/plugins/steps/jquery.steps.css"));

            // wizardSteps 
            bundles.Add(new ScriptBundle("~/plugins/wizardSteps").Include(
                      "~/Scripts/plugins/staps/jquery.steps.min.js"));

            // dropZone styles
            bundles.Add(new StyleBundle("~/Content/plugins/dropzone/dropZoneStyles").Include(
                      "~/Content/plugins/dropzone/basic.css",
                      "~/Content/plugins/dropzone/dropzone.css"));

            // dropZone 
            bundles.Add(new ScriptBundle("~/plugins/dropZone").Include(
                      "~/Scripts/plugins/dropzone/dropzone.js"));

            // summernote styles
            bundles.Add(new StyleBundle("~/plugins/summernoteStyles").Include(
                      "~/Content/plugins/summernote/summernote.css",
                      "~/Content/plugins/summernote/summernote-bs3.css"));

            // summernote 
            bundles.Add(new ScriptBundle("~/plugins/summernote").Include(
                      "~/Scripts/plugins/summernote/summernote.min.js"));

            // toastr notification 
            bundles.Add(new ScriptBundle("~/plugins/toastr").Include(
                      "~/Scripts/plugins/toastr/toastr.min.js"));

            // toastr notification styles
            bundles.Add(new StyleBundle("~/plugins/toastrStyles").Include(
                      "~/Content/plugins/toastr/toastr.min.css"));

            // color picker
            bundles.Add(new ScriptBundle("~/plugins/colorpicker").Include(
                      "~/Scripts/plugins/colorpicker/bootstrap-colorpicker.min.js"));

            // color picker styles
            bundles.Add(new StyleBundle("~/Content/plugins/colorpicker/colorpickerStyles").Include(
                      "~/Content/plugins/colorpicker/bootstrap-colorpicker.min.css"));

            // image cropper
            bundles.Add(new ScriptBundle("~/plugins/imagecropper").Include(
                      "~/Scripts/plugins/cropper/cropper.min.js"));

            // image cropper styles
            bundles.Add(new StyleBundle("~/plugins/imagecropperStyles").Include(
                      "~/Content/plugins/cropper/cropper.min.css"));

            // jsTree
            bundles.Add(new ScriptBundle("~/plugins/jsTree").Include(
                      "~/Scripts/plugins/jsTree/jstree.min.js"));

            // jsTree styles
            bundles.Add(new StyleBundle("~/Content/plugins/jsTree").Include(
                      "~/Content/plugins/jsTree/style.css"));

            // Diff
            bundles.Add(new ScriptBundle("~/plugins/diff").Include(
                      "~/Scripts/plugins/diff_match_patch/javascript/diff_match_patch.js",
                      "~/Scripts/plugins/preetyTextDiff/jquery.pretty-text-diff.min.js"));

            // Idle timer
            bundles.Add(new ScriptBundle("~/plugins/idletimer").Include(
                      "~/Scripts/plugins/idle-timer/idle-timer.min.js"));

            // Tinycon
            bundles.Add(new ScriptBundle("~/plugins/tinycon").Include(
                      "~/Scripts/plugins/tinycon/tinycon.min.js"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
