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
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            var entities = new TugceContext();
            //bu kullanıcı adı ve parolaya sahip bir kullanıcımız var mı
            var user = entities.Logins.SingleOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);
            //kullanıcının rollerini bulalım
            //var roles =string.Join(",", entities.UserRoles.Include("Role").Where(ur => ur.LoginId == user.Id)
            // .Select(r=>r.Role.RoleName).ToList());

            //Kullanıcı varsa
            if (user != null)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, model.UserName, DateTime.Now, DateTime.Now.AddMinutes(10), model.RememberMe, "", FormsAuthentication.FormsCookiePath);
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                //eğer ticket kalıcı olarak oluşturulmuşsa
                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                //Çerezi tarayıcıya kaydedilim.
                Response.Cookies.Add(cookie);

                if (Request.QueryString["ReturnUrl"] != null)
                {
                    return Redirect(Request.QueryString["ReturnUrl"]);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["error"] = "Kullanıcı adı veya parola hatalı.";
                return View();
            }
        }

        [Authorize(Roles ="admin")]
        public ActionResult CreateUser()
        {
            var entities = new TugceContext();
            LoginWithImage model = new LoginWithImage();
            model.UserRoles = entities.Roles.Select(r => new RoleCheckBoxModel
            {
                RoleId = r.Id,
                RoleName = r.RoleName,
                IsChecked = false
            }).ToList();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CreateUser(LoginWithImage model)
        {
            var entities = new TugceContext();
            //resim dosyası seçilmemiş.
            if (model.PostedFile == null)
            {
                TempData["error"] = "Resim dosyası seçilmelidir.";
                return View(model);
            }
            //resim dosyası gönderilmiş.
            //resmi kırpma alanına göre kırpalım.
            var croppedImage = model.PostedFile.CropImage(Request.Form);
            //resmin adını ve kayıt yerini hesaplayalım.
            var fileName = model.PostedFile.FileName.GenerateFileName();
            var filePath = Server.MapPath("~/Areas/Admin/Content/userimages/" + fileName);
            //resmi ilgili klasöre kaydedelim.
            croppedImage.Save(filePath);

            //kullanıcı bilgilerini rolleri hariç oluşturalım.
            var user = new LoginUser
            {
                Name = model.Login.Name,
                Password = model.Login.Password,
                Surname = model.Login.Surname,
                UserImage = fileName,
                UserName = model.Login.UserName
            };
            //Kullanıcıyı ekleyelim.
            entities.Logins.Add(user);

            //formda işaretlenmiş rollere kullanıcyı bağlayalım
            foreach (var role in model.UserRoles.Where(r => r.IsChecked))
            {
                user.UserRoles.Add(new UserInRole { LoginId = user.Id, RoleId = role.RoleId });
            }
            //İşlemleri veritabanına yansıtalım.
            entities.SaveChanges();

            TempData["success"] = "Kullanıcı başarıyla eklendi.";

            return RedirectToAction("ListUser");
        }

        [Authorize(Roles="admin,user")]
        public ActionResult ListUser()
        {
            var entities = new TugceContext();
            var logins = entities.Logins.Include("UserRoles").ToList();
            return View(logins);
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditUser(int id)
        {
            var entities = new TugceContext();
            var login = entities.Logins.SingleOrDefault(u => u.Id == id);
            //böyle bir kullanıcı yok
            if (login == null)
            {
                TempData["error"] = "Belirtilen kriterlere uygun bir kullanıcı bulunamadı.";
                return RedirectToAction("List");
            }
            //Kullanıcı var
            LoginWithImage model = new LoginWithImage();
            model.Login = login;
            //bu kullanıcının içerisinde bulunduğu rollerin id değerlerini elde edelim.
            var rolesForUser = entities.UserRoles.Where(ur => ur.LoginId == login.Id).Select(ur=>ur.RoleId);
            model.UserRoles = entities.Roles.Select(r => new RoleCheckBoxModel
            {
                RoleId = r.Id,
                RoleName = r.RoleName,
                IsChecked = rolesForUser.Contains(r.Id)?true:false
            }).ToList();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditUser(LoginWithImage model)
        {            
            var entities = new TugceContext();
            //güncellenen kaydın orjinalini elde edelim.
            var login = entities.Logins.SingleOrDefault(u => u.Id == model.Login.Id);
            //kullanıcı mevcut resmi yeni bir resimle değiştirmek istiyor.
            if (model.PostedFile!=null)
            {
                //eski resmi sil
                var fileName = login.UserImage;
                var filePath = Server.MapPath("~/Areas/Admin/Content/userimages/" + fileName);
                //mevcut resmi silelim
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
                //yeni resmi kırpalım ve kaydedelim.
                var croppedImage = model.PostedFile.CropImage(Request.Form);
                fileName = model.PostedFile.FileName.GenerateFileName();
                filePath= Server.MapPath("~/Areas/Admin/Content/userimages/" + fileName);
                croppedImage.Save(filePath);
                login.UserImage = fileName;
            }
            //mevcut resim korunacaksa gelen modeldeki bilgileri veritabanına yansıtalım.

            //kullanıcının şu an içerisinde bulunduğu tüm rollerden kullanıcıyı çıkaralım.
            entities.UserRoles.RemoveRange(entities.UserRoles.Where(ur => ur.LoginId == model.Login.Id));
            //modelden gelen rollere kullanıcıyı ekleyelim.
            foreach (var role in model.UserRoles.Where(r => r.IsChecked))
            {
                login.UserRoles.Add(new UserInRole { LoginId = login.Id, RoleId = role.RoleId });
            }
            //geri kalan bilgileri de güncelleyerek kayıt işlemini tamamlayalım.
            login.Name = model.Login.Name;
            login.Surname = model.Login.Surname;
            login.Password = model.Login.Password;
            login.UserName = model.Login.UserName;
            //kaydet
            entities.SaveChanges();
            return RedirectToAction("ListUser");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteUser(int id)
        {
            var entities = new TugceContext();
            var login = entities.Logins.SingleOrDefault(u => u.Id == id);
            //böyle bir kullanıcı yok
            if (login==null)
            {
                return Json(new {
                    Status="error",
                    Message="Belirtilen kriterlere uygun bir kullanıcı bulunamadı."
                });
            }
            //kullanıcı var
            //kullanıcıyı veritabanından sil
            entities.Logins.Remove(login);
            entities.SaveChanges();
            //işlem başarılı
            return Json(new {
                Message="Kullanıcı başarıyla silindi.",
                Status="ok"
            });
        }

        [Authorize(Roles = "admin")]
        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CreateRole(Role role)
        {
            var entities = new TugceContext();
            entities.Roles.Add(role);
            entities.SaveChanges();
            TempData["success"] = "Rol başarıyla eklendi.";
            return RedirectToAction("ListRole");
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditRole(int id)
        {
            var entities = new TugceContext();
            var role = entities.Roles.SingleOrDefault(r => r.Id == id);
            if(role==null)
            {
                TempData["error"] ="Belirtilen kriterlere uygun bir rol bulunamadı.";
                return RedirectToAction("ListRole");
            }
            return View(role);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditRole(Role role)
        {
            var entities = new TugceContext();
            var roleInDb = entities.Roles.SingleOrDefault(r => r.Id == role.Id);
            if(roleInDb==null)
            {
                TempData["error"] = "Belirtilen kriterlere uygun bir rol bulunamadı.";
            }
            else
            {
                roleInDb.RoleName = role.RoleName;                
                entities.SaveChanges();
                TempData["success"] = "Rol başarıyla güncellendi";
            }
            return RedirectToAction("ListRole");
        }

        [Authorize(Roles = "admin,user")]
        public ActionResult ListRole()
        {
            var entities = new TugceContext();
            var roles = entities.Roles.ToList();
            return View(roles);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteRole(int id)
        {
            var entities = new TugceContext();
            var role = entities.Roles.Include("UserRoles").SingleOrDefault(r=>r.Id==id);
            //gönderilen id değerine sahip bir rol yok
            if (role==null)
            {
                return Json(new {
                    Status = "error",
                    Message = "Belirtilen kriterlere uygun bir rol bulunamadı."
                });
            }
            //veritabanında böyle bir rol bulundu.

            //bu rolde kayıtlı bir kullanıcı var mı?
            if(role.UserRoles.Count!=0) //evet var
            {
                return Json(new
                {
                    Status = "error",
                    Message = "Bu rolde kayıtlı kullanıcı(lar) olduğundan rolü silemezsiniz."
                });
            }
            else
            {
                entities.Roles.Remove(role);
                return Json(new
                {
                    Status = "ok",
                    Message = "Rol başarıyla silindi."
                });
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Account");
        }

        public ActionResult UpdateProfile()
        {
            var entities = new TugceContext();
            var profile = entities.Logins.SingleOrDefault(u=>u.UserName==User.Identity.Name);
            LoginWithImage model = new LoginWithImage();
            model.Login = profile;
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateProfile(LoginWithImage model)
        {
            var entities = new TugceContext();
            //güncellenen kaydın orjinalini elde edelim.
            var login = entities.Logins.SingleOrDefault(u => u.Id == model.Login.Id);
            //kullanıcı mevcut resmi yeni bir resimle değiştirmek istiyor.
            if (model.PostedFile != null)
            {
                //eski resmi sil
                var fileName = login.UserImage;
                var filePath = Server.MapPath("~/Areas/Admin/Content/userimages/" + fileName);
                //mevcut resmi silelim
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
                //yeni resmi kırpalım ve kaydedelim.
                var croppedImage = model.PostedFile.CropImage(Request.Form);
                fileName = model.PostedFile.FileName.GenerateFileName();
                filePath = Server.MapPath("~/Areas/Admin/Content/userimages/" + fileName);
                croppedImage.Save(filePath);
                login.UserImage = fileName;
            }
            //geri kalan bilgileri de güncelleyerek kayıt işlemini tamamlayalım.
            login.Name = model.Login.Name;
            login.Surname = model.Login.Surname;
            login.Password = model.Login.Password;
            login.UserName = model.Login.UserName;
            //kaydet
            entities.SaveChanges();
            TempData["success"] = "Bilgileriniz başarıyla güncellendi.";
            LoginWithImage loginModel = new LoginWithImage();
            loginModel.Login = login;
            return View(loginModel);
        }
    }
}