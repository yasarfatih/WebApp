using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tugce.DataContext;
using Tugce.Domain.POCO;
using Tugce.Domain.VM;
using Tugce.Utils;

namespace Tugce.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Kategorileri hazırlayarak View'e gönderelim.
            var entities = new TugceContext();
            //categories   ->   IEnumerable<SelectListItem>
            var categories = entities.Categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.Id.ToString()
            });
            TempData["categories"] = categories;

            base.OnActionExecuting(filterContext);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Yeni Ürün";

            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductWithImage productModel)
        {
            //Kırpılmış resmi elde edelim ve sunucuya kaydedelim
            Image croppedImage = productModel.PostedImage.CropImage(Request.Form);
            var newFileName = productModel.PostedImage.FileName.GenerateFileName();
            string filePath = Server.MapPath("~/Content/product/full/" + newFileName);
            croppedImage.Save(filePath);
            string thumbFilePath= Server.MapPath("~/Content/product/thumb/" + newFileName);
            croppedImage.GetThumbnailImage(570, 400).Save(thumbFilePath);
            //Ürüne ait bilgileri veritabanına yazalım.
            TugceContext entities = new TugceContext();
            productModel.Product.FileName = newFileName;
            productModel.Product.LastUpUser = User.Identity.Name;
            productModel.Product.CreateUser = User.Identity.Name;
            entities.Products.Add(productModel.Product);
            entities.SaveChanges();
            TempData["success"] = "Ürün başarıyla eklendi.";
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            ViewBag.Title = "Sistemde Kayıtlı Kategoriler";
            var entities = new TugceContext();
            var products = entities.Products.Include("Category").ToList();
            return View(products);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var entities = new TugceContext();
            var productInDb = entities.Products.SingleOrDefault(p => p.Id == id);
            //veritabanında bu id değerine sahip bir ürün yok
            if (productInDb == null)
            {
                return Json(new
                {
                    Status = "error",
                    Message = "Belirtilen kriterlere uygun bir ürün bulunamadı."
                });
            }
            //ürün bulundu. 
            //önce mevcut ürün resmini silelim.
            var filePath = Server.MapPath("~/Content/product/full/" + productInDb.FileName);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
            //ürünün thumb resmini silelim.
            var thumbFilePath = Server.MapPath("~/Content/product/thumb/" + productInDb.FileName);
            if (System.IO.File.Exists(thumbFilePath))
                System.IO.File.Delete(thumbFilePath);

            //Ürünü silelim
            entities.Products.Remove(productInDb);
            entities.SaveChanges();
            return Json(new
            {
                Status = "ok",
                Message = "Ürün başarıyla silindi."
            });
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Kategoriyi Güncelle";
            var entities = new TugceContext();
            var productInDb = entities.Products.SingleOrDefault(p => p.Id == id);
            if (productInDb == null)
            {
                TempData["error"] = "Belirtilen kriterlere sahip bir ürün bulunamadı.";
                return RedirectToAction("List");
            }

            ProductWithImage model = new ProductWithImage();
            model.Product = productInDb;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProductWithImage model)
        {
            ViewBag.Title = "Ürünü Güncelleyin";
            var entities = new TugceContext();
            //veritabanında kayıtlı olan mevcut ürün bilgileri
            var productInDb = entities.Products.SingleOrDefault(p => p.Id == model.Product.Id);
            if (productInDb == null)
            {
                TempData["error"] = "Belirtilen kriterlere uygun bir ürün bulunamadı.";
                return View(model);
            }
            //kullanıcı bu ürün için kayıtlı mevcut resmi değiştirmek istiyor
            if (model.PostedImage != null)
            {
                //Kırpılmış resmi elde edelim ve sunucuya kaydedelim
                Image croppedImage = model.PostedImage.CropImage(Request.Form);
                var newFileName = model.PostedImage.FileName.GenerateFileName();
                string filePath = Server.MapPath("~/Content/product/full/" + newFileName);
                
                //yeni resim sunucuya kaydediliyor.
                croppedImage.Save(filePath);

                //yeni thumbnail resmi sunucuya kaydediliyor.
                var thumbFilePath= Server.MapPath("~/Content/product/thumb/" + newFileName);
                croppedImage.GetThumbnailImage(570, 400).Save(thumbFilePath);

                //yeni resim dosyasına verilen isim modelde yer alan FileName özelliğine yükleniyor.
                model.Product.FileName = newFileName;

                //eski resmi sil
                var oldFilePath = Server.MapPath("~/Content/product/full/" + productInDb.FileName);
                if (System.IO.File.Exists(oldFilePath))
                    System.IO.File.Delete(oldFilePath);
                //eski thumbnail resmi sil
                var oldThumbFilePath = Server.MapPath("~/Content/product/thumb/" + productInDb.FileName);
                if (System.IO.File.Exists(oldThumbFilePath))
                    System.IO.File.Delete(oldThumbFilePath);
                
            }
            productInDb.CategoryId = model.Product.CategoryId;
            productInDb.Description = model.Product.Description;
            productInDb.IsActive = model.Product.IsActive;
            productInDb.LastUpDate = DateTime.Now;
            productInDb.LastUpUser = User.Identity.Name;
            productInDb.Priority = model.Product.Priority;
            productInDb.ProductName = model.Product.ProductName;
            productInDb.FileName = model.Product.FileName != null ? model.Product.FileName : productInDb.FileName;
            entities.SaveChanges();
            TempData["success"] = "Ürün başarıyla güncellendi";
            return RedirectToAction("List");
        }
    }
}