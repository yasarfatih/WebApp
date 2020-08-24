using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tugce.DataContext;

namespace Tugce.Web.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            var entities = new TugceContext();
            var aboutPage = entities.Statics.FirstOrDefault();
            if (aboutPage == null)
                return new HttpStatusCodeResult(404);

            return View(aboutPage);
        }
    }
}