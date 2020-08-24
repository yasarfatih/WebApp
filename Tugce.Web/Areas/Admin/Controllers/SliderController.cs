using System;
using System.Collections.Generic;
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
    public class SliderController : BaseController
    {
        public ActionResult Create()
        {
            ViewBag.Title = "Slider Ekle";
            return View();
        }

        [HttpPost]
        public ActionResult Create(SliderWithImage model)
        {
            //Kullanıcı resim göndermedi
            if(model.PostedFile==null)
            {
                TempData["error"] = "Resim seçilmelidir.";
                return View(model);
            }

            //Kullanıcının seçtiği resmi kırparak sunucuya kaydedeceğimiz resmi elde edelim.
            var resultImage=model.PostedFile.CropImage(Request.Form);
            //resim için bir isim üretelim.
            var fileName = model.PostedFile.FileName.GenerateFileName();
            //resmin kaydedileceği fiziksel yol hesaplanıyor.
            var filePath = Server.MapPath("~/Content/slider/" + fileName);
            //resim ilgili klasörüne kaydediliyor.
            resultImage.Save(filePath);

            //Şimdi veritabanına slider bilgilerini kaydedelim.
            var entities = new TugceContext();
            //sunucuya postalanan ve kaydettiğimiz resme vaerilen ismi Slider modelinde
            //ilgili property'ye yükleyelim.
            model.Slider.FileName = fileName;
            //Slider entity'ye yeni slider bilgilerini ekleyelim.
            entities.Sliders.Add(model.Slider);
            entities.SaveChanges();

            //Kayıt tamamlanmış demektir.
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            ViewBag.Title = "Kayan Resimler";
            var entities = new TugceContext();
            var sliders = entities.Sliders.ToList();
            return View(sliders);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Slider Düzenle";
            var entites = new TugceContext();
            var sliderInDb = entites.Sliders.SingleOrDefault(s=>s.Id==id);
            //Veritabanında slider bulanamadı
            if(sliderInDb==null)
            {
                TempData["error"] = "Belirtilen kriterlere uygun bir slider bulunamadı.";
                return RedirectToAction("List");
            }
            SliderWithImage sliderModel = new SliderWithImage();
            sliderModel.Slider = sliderInDb;
            return View(sliderModel);
        }

        [HttpPost]
        public ActionResult Edit(SliderWithImage sliderModel)
        {
            //Kullanıcı şu an kayıtlı olan resmi değiştirmiş olabilir.
            //Kullanıcı Slider resmi dışındaki bilgileri değiştirmiş ancak resme dokunmamış olabilir.

            var entities = new TugceContext();
            var sliderInDb = entities.Sliders.SingleOrDefault(s=>s.Id==sliderModel.Slider.Id);

            //Yeni resim göndermiş
            if(sliderModel.PostedFile!=null)
            {
                var oldFileName = sliderInDb.FileName;
                var oldFilePath = Server.MapPath("~/Content/slider/" + oldFileName);
                if (System.IO.File.Exists(oldFilePath))
                    System.IO.File.Delete(oldFilePath);

                //yeni resim dosyasını sunucuya yükleyelim.
                var fileName = oldFileName.GenerateFileName();
                var filePath = Server.MapPath("~/Content/slider/"+fileName);
                var croppedImage = sliderModel.PostedFile.CropImage(Request.Form);
                croppedImage.Save(filePath);
                sliderInDb.FileName = fileName;
            }
            //slider'ın veritabanında yer alan bilgilerini güncelleyelim.
            sliderInDb.Description = sliderModel.Slider.Description;
            sliderInDb.Detail = sliderModel.Slider.Detail;
            sliderInDb.IsActive = sliderModel.Slider.IsActive;
            sliderInDb.Title = sliderModel.Slider.Title;

            entities.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var entities = new TugceContext();
            var sliderInDb = entities.Sliders.SingleOrDefault(s=>s.Id==id);
            if(sliderInDb==null)
            {
                return Json(new
                {
                    Status = "error",
                    Message = "Belitilen kriterlere uygun bir slider bulunamadı."
                });
            }
            //Önce slider'ın resim dosyasını silelim.
            var fileName = sliderInDb.FileName;
            var filePath = Server.MapPath("~/Content/slider/" + fileName);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
            //Şimdi slider bilgilerini veritabanından kaldıralım.
            entities.Sliders.Remove(sliderInDb);
            entities.SaveChanges();
            return Json(new
            {
                Status = "ok",
                Message = "Slider bilgileri başarıyla silindi."
            });
        }

    }
}