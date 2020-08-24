using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Tugce.Domain.POCO
{
    public class Slider:Base
    {
        [Column(TypeName ="varchar",Order =2)]
        [StringLength(50,ErrorMessage ="Başlık bilgisi 50 karakteri geçemez.")]
        [Display(Name ="Başlık")]
        [Required(ErrorMessage ="Başlık alanı gereklidir.")]
        public string Title { get; set; }

        [Column(TypeName = "varchar",Order =3)]
        [StringLength(255, ErrorMessage = "Açıklama bilgisi 255 karakteri geçemez.")]
        [Display(Name = "Açıklama")]
        [Required(ErrorMessage ="Açıklama alanı gereklidir.")]
        public string Description { get; set; }

        [Column(TypeName = "varchar",Order =4)]
        [StringLength(8000, ErrorMessage = "Detay bilgisi 8000 karakteri geçemez.")]
        [Display(Name = "Detay Sayfası İçeriği")]
        [AllowHtml]
        [Required(ErrorMessage ="Detay bilgisi girilmelidir.")]
        public string Detail { get; set; }

        [Column(TypeName = "varchar",Order =5)]
        [StringLength(50)]
        [ScaffoldColumn(false)]
        public string FileName { get; set; }
    }
}
