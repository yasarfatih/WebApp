using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Tugce.Utils
{
    public class CustomModelBinder:DefaultModelBinder
    {
        protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            controllerContext.Controller.ViewData.Model=bindingContext.Model;
            base.OnModelUpdated(controllerContext, bindingContext);
        }
    }
}
