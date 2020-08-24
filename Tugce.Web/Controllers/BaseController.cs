using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tugce.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Request.Cookies["prev"] != null)
                Response.Cookies.Remove("prev");

            HttpCookie cookie = new HttpCookie("prev", Request.Url.AbsolutePath);
            Response.Cookies.Add(cookie);

            base.OnActionExecuted(filterContext);
        }

    }
}