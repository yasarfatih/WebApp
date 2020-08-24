using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using Tugce.Utils;

namespace Tugce.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //CKEditor
            bundles.Add(new ScriptBundle("~/Admin/ckEditor")
                .Include("~/Areas/Admin/Content/plugins/ck/ckeditor/ckeditor.js")
                .Include("~/Areas/Admin/Content/plugins/ck/ckfinder/ckfinder.js"));

            //Jconfirm
            bundles.Add(new StyleBundle("~/Admin/jconfirm/css")
                .Include("~/Areas/Admin/Content/plugins/jconfirm/css/jquery-confirm.css"));
            bundles.Add(new ScriptBundle("~/Admin/jconfirm/js")
                .Include("~/Areas/Admin/Content/plugins/jconfirm/js/jquery-confirm.js"));

            //DataTable
            bundles.Add(new StyleBundle("~/Admin/DataTable/css")
                .Include("~/Areas/Admin/Content/plugins/data-table/jquery.dataTables.min.css"));
            bundles.Add(new ScriptBundle("~/Admin/DataTable/js")
                .Include("~/Areas/Admin/Content/plugins/data-table/jquery.dataTables.min.js"));

            //select2
            bundles.Add(new StyleBundle("~/Admin/Select2/css")
                .Include("~/Areas/Admin/Content/plugins/select2/select2.min.css"));
            bundles.Add(new ScriptBundle("~/Admin/Select2/js")
                .Include("~/Areas/Admin/Content/plugins/select2/select2.min.js"));

            //cropper
            bundles.Add(new StyleBundle("~/Admin/cropper/css")
                .Include("~/Areas/Admin/Content/plugins/cropper/cropper.css"));
            bundles.Add(new ScriptBundle("~/Admin/cropper/js")
                .Include("~/Areas/Admin/Content/plugins/cropper/cropper.js")
                .Include("~/Areas/Admin/Content/plugins/cropper/jquery-cropper.js")
                .CustomOrderer());

            //inputmask
            bundles.Add(new ScriptBundle("~/Admin/inputmask/js")
                .Include("~/Areas/Admin/Content/plugins/moment/moment.min.js")
                .Include("~/Areas/Admin/Content/plugins/inputmask/min/jquery.inputmask.bundle.min.js")
                .CustomOrderer());
        }
    }
}