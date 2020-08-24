using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tugce.DataContext;

namespace Tugce.Web.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Detail(string category,int id)
        {
            var entities = new TugceContext();
            
            var productsInCategory = entities.Products.Include("Category")
                .Where(p=>p.CategoryId==id);

            if (productsInCategory.Count()==0)
                return new HttpStatusCodeResult(404);

            var categoryInDb = entities.Categories.SingleOrDefault(c => c.Id == id);
            ViewBag.Title = categoryInDb.CategoryName;

            return View(productsInCategory);
        }
    }
}