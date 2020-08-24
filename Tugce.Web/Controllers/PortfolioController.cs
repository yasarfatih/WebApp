using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tugce.DataContext;

namespace Tugce.Web.Controllers
{
    public class PortfolioController : Controller
    {
        // GET: Portfolio
        public ActionResult Index()
        {
            var entities = new TugceContext();
            var categoriesWithProduct = entities.Categories.Include("Products")
                .ToList();
            return View(categoriesWithProduct);
        }

        // GET: Product
        public ActionResult Detail(int id, string name)
        {
            var entities = new TugceContext();
            var product = entities.Products.Include("Category").SingleOrDefault(p => p.Id == id);

            //adres satırından gönderilen bilgiler bir ürünle eşleşmiyorsa
            if (product == null)
                return new HttpStatusCodeResult(404);

            return View(product);
        }
    }
}