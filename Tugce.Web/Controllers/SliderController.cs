using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tugce.DataContext;

namespace Tugce.Web.Controllers
{
    public class SliderController : BaseController
    {
        // GET: Slider
        public ActionResult Detail(int id,string title)
        {
            var entities = new TugceContext();
            var sliderInDb = entities.Sliders.Where(s => s.Id == id && s.IsActive).FirstOrDefault();
            if(sliderInDb==null)
            {
                return new HttpStatusCodeResult(404);
            }
            return View(sliderInDb);
        }
    }
}