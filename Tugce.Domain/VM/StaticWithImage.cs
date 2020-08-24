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
    public class StaticWithImage
    {
        public SiteStatic SiteStatic { get; set; }

        [UIHint("FileUpload")]
        [Display(Name ="Video Dosyası")]
        public HttpPostedFileBase PostedFile { get; set; }

        [UIHint("ImageUpload")]
        [Display(Name = "Logo Resmi")]
        public HttpPostedFileBase PostedImage { get; set; }
    }
}
