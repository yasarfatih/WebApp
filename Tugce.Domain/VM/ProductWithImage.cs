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
    public class ProductWithImage
    {
        public Product Product { get; set; }

        [Display(Name ="Resim Yükle")]
        [UIHint("ImageUpload")]
        public HttpPostedFileBase PostedImage { get; set; }
    }
}
