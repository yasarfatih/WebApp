using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Tugce.DataContext;
using Tugce.Web.Models.VM;

namespace Tugce.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();            
        }

        public ActionResult SiteStatic()
        {
            var entities = new TugceContext();
            var siteStatic = entities.Statics.FirstOrDefault();
            return PartialView("_Footer", siteStatic);
        }

        public ActionResult Menu()
        {
            var entities = new TugceContext();
            TopMenuModel model = new TopMenuModel();
            //_TopMenu isimli Partial'a gönderilecek model hazırlanıyor.
            model.Categories = entities.Categories.Where(c => c.IsActive).ToList();
            model.LogoImage = (entities.Statics.FirstOrDefault()==null) ? "" : entities.Statics.FirstOrDefault().LogoFile;
            model.SloganTitle = (entities.Statics.FirstOrDefault()==null)?"": entities.Statics.FirstOrDefault().SloganTitle;
            model.Firma = (entities.Statics.FirstOrDefault() == null) ? "" : entities.Statics.FirstOrDefault().Firma;
            model.Controller = ControllerContext.ParentActionViewContext
                .RouteData.Values["controller"].ToString();

            return PartialView("_TopMenu", model);
        }
    }
}