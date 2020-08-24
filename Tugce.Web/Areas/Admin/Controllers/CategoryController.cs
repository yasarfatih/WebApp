using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tugce.DataContext;
using Tugce.Domain.POCO;
using Tugce.Utils;

namespace Tugce.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : BaseController
    {
        public ActionResult Create()
        {
            ViewBag.Title = "Kategori Oluştur";
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            ViewBag.Title = "Kategori Oluştur";
            TugceContext entities = new TugceContext();
            entities.Categories.Add(category);
            entities.SaveChanges();
            TempData["success"] = "Kayıt başarıyla tamamlandı";
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            ViewBag.Title = "Kategori Düzenle";
            TugceContext entities = new TugceContext();
            var categories = entities.Categories.ToList();
            return View(categories);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            TugceContext entities = new TugceContext();
            var categoryInDb = entities.Categories.SingleOrDefault(c => c.Id == id);
            if (categoryInDb == null)
            {
                return Json(new
                {
                    Status = "error",
                    Message = "Belirtilen kriterlere uygun bir kategori bulunamadı."
                });
            }
            entities.Categories.Remove(categoryInDb);
            entities.SaveChanges();
            return Json(new
            {
                Status = "ok",
                Message = "Kategori başarıyla silindi"
            });
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Kategoriyi Güncelle";
            TugceContext entities = new TugceContext();
            var categoryInDb = entities.Categories.SingleOrDefault(c => c.Id == id);
            if (categoryInDb == null)
            {
                TempData["error"] = "Belirtilen kriterlere uygun bir kategori bulunamadı";
                return RedirectToAction("List");
            }
            return View(categoryInDb);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            ViewBag.Title = "Kategoriyi Güncelle";
            TugceContext entites = new TugceContext();
            if (entites.Entry(category).State == EntityState.Detached)
                entites.Categories.Attach(category);
            entites.Entry(category).State = EntityState.Modified;
            entites.SaveChanges();
            TempData["success"] = "Kategori başarıyla güncellendi.";
            return RedirectToAction("List");
        }

    }
}