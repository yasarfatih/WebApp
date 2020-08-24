using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Tugce.DataContext;
using Tugce.Domain.POCO;
using Tugce.Domain.VM;
using Tugce.Utils;

namespace Tugce.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class StaticController : BaseController
    {
        public ActionResult Edit()
        {
            ViewBag.Title = "Site Sabitleri";
            var entities = new TugceContext();
            var statics = entities.Statics.FirstOrDefault();
            var model = new StaticWithImage();
            if (statics == null)
                model.SiteStatic = new SiteStatic();
            else
                model.SiteStatic = statics;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(StaticWithImage model)
        {
            ViewBag.Title ="Site Sabitleri";

            var entities = new TugceContext();

            //sabit bilgiler ilk defa ekleniyor
            if (model.SiteStatic.Id==0)
            {
                //Logo resmi için
                if (model.PostedImage == null)
                {
                    TempData["error"] = "Logo dosyası seçilmelidir.";
                    return View(model);
                }
                //logo dosyasına isim üretelim.
                var fileName = model.PostedFile.FileName.GenerateFileName();
                //video dosyasının kayıt yerini hesaplayalım.
                var filePath = Server.MapPath("~/Content/images/" + fileName);
                //Dosyayı kaydedelim.
                var croppedImage = model.PostedImage.CropImage(Request.Form);
                croppedImage.Save(filePath);
                //veritabanında dosyanın adını saklayalım.
                model.SiteStatic.LogoFile = fileName;
                //logo resmi için yazılan kısmın sonu

                //Video dosyası için
                if (model.PostedFile==null)
                {
                    TempData["error"] = "Video dosyası seçilmelidir.";
                    return View(model);
                }
                //video dosyasına isim üretelim.
                fileName = model.PostedFile.FileName.GenerateFileName();
                //video dosyasının kayıt yerini hesaplayalım.
                filePath = Server.MapPath("~/Content/files/" + fileName);
                //Dosyayı kaydedelim.
                model.PostedFile.SaveAs(filePath);
                //veritabanında dosyanın adını saklayalım.
                model.SiteStatic.VideoFile = fileName;
                //Video dosyası için olan kısmın sonu

                //CreateUser ve LastUpUser şu an login olan kullanıcıdır.
                model.SiteStatic.CreateUser = User.Identity.Name;
                model.SiteStatic.LastUpUser = User.Identity.Name;

                //veritabanına kaydı gerçekleştirelim.                
                entities.Statics.Add(model.SiteStatic);
                entities.SaveChanges();
                TempData["success"] = "Kayıt başarıyla eklendi.";
            }
            else //sabit bilgiler güncelleniyor
            {
                //Kullanıcı daha önce eklenmiş olan dosyayı yenisiyle değiştirmek istiyor.
                if(model.PostedFile!=null)
                {
                    //eski dosyayı diskten silelim.
                    var oldFileName = model.SiteStatic.VideoFile;
                    var oldFilePath= Server.MapPath("~/Content/files/" + oldFileName);
                    if (System.IO.File.Exists(oldFilePath))
                        System.IO.File.Delete(oldFilePath);

                    //yeni dosyayı kaydedilim.
                    var newFileName = model.PostedFile.FileName.GenerateFileName();
                    var newFilePath = Server.MapPath("~/Content/files/" + newFileName);
                    model.PostedFile.SaveAs(newFilePath);
                    //veritabanındaki dosya adını güncelleyelim.
                    model.SiteStatic.VideoFile = newFileName;
                }

                //Kullanıcı daha önce eklenmiş olan dosyayı yenisiyle değiştirmek istiyor.
                if (model.PostedImage != null)
                {
                    //eski dosyayı diskten silelim.
                    var oldFileName = model.SiteStatic.LogoFile;
                    var oldFilePath = Server.MapPath("~/Content/images/" + oldFileName);
                    if (System.IO.File.Exists(oldFilePath))
                        System.IO.File.Delete(oldFilePath);

                    //yeni dosyayı kaydedilim.
                    var newFileName = model.PostedImage.FileName.GenerateFileName();
                    var newFilePath = Server.MapPath("~/Content/images/" + newFileName);
                    var croppedImage = model.PostedImage.CropImage(Request.Form);
                    croppedImage.Save(newFilePath);
                    //veritabanındaki dosya adını güncelleyelim.
                    model.SiteStatic.LogoFile = newFileName;
                }

                //var staticInDb = entities.Statics.FirstOrDefault();
                //staticInDb.AboutContent = model.SiteStatic.AboutContent;
                //staticInDb.AboutTitle = model.SiteStatic.AboutTitle;
                //staticInDb.Facebook = model.SiteStatic.Facebook;
                //staticInDb.Footer = model.SiteStatic.Footer;
                //staticInDb.IsActive = model.SiteStatic.IsActive;
                //staticInDb.Latitude = model.SiteStatic.Latitude;
                //staticInDb.Longitude = model.SiteStatic.Longitude;
                //staticInDb.Phone1 = model.SiteStatic.Phone1;
                //staticInDb.Phone2 = model.SiteStatic.Phone2;
                //staticInDb.Mobile1 = model.SiteStatic.Mobile1;
                //staticInDb.Mobile2 = model.SiteStatic.Mobile2;
                //staticInDb.Pinterest = model.SiteStatic.Pinterest;
                //staticInDb.Priority = model.SiteStatic.Priority;
                //staticInDb.Twitter = model.SiteStatic.Pinterest;
                //staticInDb.VideoFile = model.SiteStatic.VideoFile;

                //Son güncellemeyi yapan kişi şu an login olmuş kişidir.
                model.SiteStatic.LastUpUser = User.Identity.Name;

                if (entities.Entry(model.SiteStatic).State == System.Data.Entity.EntityState.Detached)
                    entities.Statics.Attach(model.SiteStatic);
                entities.Entry(model.SiteStatic).State = System.Data.Entity.EntityState.Modified;
                entities.SaveChanges();
                TempData["success"] = "Kayıt başarıyla güncellendi.";
                
            }

            return View(model);
        }
    }
}