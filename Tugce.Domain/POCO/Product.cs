using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tugce.Domain.POCO
{
    [Table("Products")]
    public class Product : Base
    {
        [ForeignKey("Category")]
        [Display(Name = "Kategorisi")]
        [Column(Order = 2)]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Display(Name = "Ürün adı")]
        [Column(TypeName = "varchar", Order = 3),
            StringLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olabilir."),
            Required(ErrorMessage = "Ürün adı gereklidir.")]
        public string ProductName { get; set; }

        [Display(Name = "Açıklama")]
        [Column(TypeName = "varchar", Order = 4)]
        [StringLength(8000, ErrorMessage = "Açıklama bilgisi en fazla 8000 karakter olabilir."),
            Required(ErrorMessage = "Açıklama bilgisi gereklidir.")]
        public string Description { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar", Order = 5)]
        [ScaffoldColumn(false)]
        [Display(Name = "Ürün Resmi")]
        public string FileName { get; set; }

    }
}
