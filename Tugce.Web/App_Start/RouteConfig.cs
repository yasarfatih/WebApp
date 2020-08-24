using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tugce.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Contact",
                url: "{firma}/bize-ulasin",
                defaults: new { controller = "Contact", action = "Index" },
                namespaces: new string[] { "Tugce.Web.Controllers" }
            );

            routes.MapRoute(
                name: "About",
                url: "Hakkimizda/{firma}-kimdir",
                defaults: new { controller = "About", action = "Index" },
                namespaces: new string[] { "Tugce.Web.Controllers" }
            );

            routes.MapRoute(
                name: "ServiceDetail",
                url: "Hizmetlerimiz/{category}-hizmetlerimiz/{id}",
                defaults: new { controller = "Service", action = "Detail" },
                namespaces: new string[] { "Tugce.Web.Controllers" }
            );

            routes.MapRoute(
                name: "ProductDetail",
                url: "Product/{id}/{name}",
                defaults: new { controller = "Portfolio", action = "Detail" },
                namespaces: new string[] { "Tugce.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Slider",
                url: "Slider/{id}/{title}",
                defaults: new { controller = "Slider", action = "Detail"},
                namespaces: new string[] { "Tugce.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces:new string[] { "Tugce.Web.Controllers" }
            );
        }
    }
}
