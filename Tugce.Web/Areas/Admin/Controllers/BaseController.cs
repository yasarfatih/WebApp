using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tugce.DataContext;
using Tugce.Domain.POCO;
using Tugce.Utils;

namespace Tugce.Web.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //modelde hatalar varsa
            if (!ModelState.IsValid)
            {
                var tempDic = filterContext.Controller.TempData;
                tempDic.Add("error", ModelState.ParseModelStateErrors());

                var viewDic = filterContext.Controller.ViewData;
                //viewDic.Model = filterContext.Controller.ViewData.Model;

                filterContext.Result = new ViewResult
                {
                    ViewName = filterContext.RouteData.Values["action"].ToString(),
                    TempData = tempDic,
                    ViewData = viewDic
                };
            }

            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {         
            //Hatayı LogErrors tablsuna yazdıralım
            TugceContext entities = new TugceContext();
            var error = new LogError
            {
                Action = filterContext.RouteData.Values["action"].ToString(),
                Controller = filterContext.RouteData.Values["controller"].ToString(),
                CreateUser=User.Identity.Name,
                LastUpUser = User.Identity.Name,
                StackTrace = filterContext.Exception.StackTrace,
                Message=filterContext.Exception.Message
            };

            entities.Errors.Add(error);            

            filterContext.ExceptionHandled = true;

            //istek ajax yöntemi ile yapılan bir istek mi
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                //isteğin ajax isteği olduğuna dair bilyiyi set edelim.
                error.IsAjaxRequest = true;
                entities.SaveChanges();
                filterContext.Result = Json(new
                {
                    Status = "error",
                    Message = "İşlem esnasında bir hata oluştu. Hata kayıt altına alındı. <br/>Hata No : " + error.Id
                });
            }
            else //hata normal bir action çağrılırken ortaya çıktı
            {
                entities.SaveChanges();

                //Kullanıcıyı geldiği View'e model ve tempdata ile yolluyoruz.
                var tempDic = new TempDataDictionary();
                tempDic.Add("error", "İşlem esnasında bir hata oluştu. Hata kayıt altına alındı. <br/>Hata No : " + error.Id);

                var viewDic = new ViewDataDictionary();
                viewDic.Model = filterContext.Controller.ViewData.Model;

                filterContext.Result = new ViewResult
                {
                    ViewName = filterContext.RouteData.Values["action"].ToString(),
                    TempData = tempDic,
                    ViewData = viewDic
                };
            }

            base.OnException(filterContext);
        }

    }
}