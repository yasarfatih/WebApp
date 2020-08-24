using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Tugce.DataContext;
using Tugce.Utils;

namespace Tugce.Web.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index(string firma)
        {
            var entities = new TugceContext();
            var siteStatic = entities.Statics.FirstOrDefault();
            ViewBag.Location = siteStatic.Location;
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(Tugce.Domain.POCO.Message model)
        {
            //Eğer modelde hatalar varsa
            if(!ModelState.IsValid)
            {
                return Json(new {
                    Status="error",
                    Message=ModelState.ParseModelStateErrors()
                });
            }

            if(string.IsNullOrEmpty(Request.Form["g-recaptcha-response"]))
            {
                return Json(new
                {
                    Status = "error",
                    Message = "Senin robot olmadığını nereden bilelim."
                });
            }

            var entities = new TugceContext();

            try
            {
                entities.Messages.Add(model);
                entities.SaveChanges();
                //hata yok.mesaj kaydedildi.

                //mesaj gönderildiğine dair cookie ayarlanıyor.
                HttpCookie cook = new HttpCookie("messagesended", "1");
                cook.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(cook);

                return Json(new {
                    Status = "ok",
                    Message = "Mesajınız başarıyla kaydedildi. En kısa zamanda ilgilenilecektir."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Status = "error",
                    Message = "Kayıt esnasında bir hata oluştu. <br/>"+ex.Message
                });
            }
        }
    }
}