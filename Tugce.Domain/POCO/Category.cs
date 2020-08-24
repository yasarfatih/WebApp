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
    [Table("Categories")]
    public class Category:Base
    {
        [Column(TypeName ="varchar",Order =2)]
        [StringLength(50,ErrorMessage ="Kategori adı en fazla 50 karakter olabilir.")]
        [Required(ErrorMessage ="Kategori adı boş bırakılamaz.")]
        [Display(Name ="Kategori adı")]
        public string CategoryName { get; set; }

        [Column(TypeName = "varchar",Order =3)]
        [StringLength(5000, ErrorMessage = "Açıklama alanı en fazla 5000 karakter olabilir.")]
        [Display(Name ="Açıklama")]
        [Required(ErrorMessage ="Açıklama alanı boş bırakılamaz.")]
        [AllowHtml]
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
