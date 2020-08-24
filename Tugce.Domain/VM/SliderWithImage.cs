using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tugce.Domain.POCO;

namespace Tugce.Domain.VM
{
    public class SliderWithImage
    {
        public Slider Slider { get; set; }

        [UIHint("ImageUpload")]
        [Display(Name ="Resim Seçiniz")]
        public HttpPostedFileBase PostedFile { get; set; }
    }
}
