using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tugce.Domain.POCO
{
    public class Base
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order =1)]
        public int Id { get; set; }
      
        [ScaffoldColumn(false)]
        [Column(Order =25)]
        public DateTime CreateDate { get; set; }

        [ScaffoldColumn(false)]
        [Column(Order = 26)]
        public DateTime LastUpDate { get; set; }

        [Column(TypeName ="varchar",Order = 27),StringLength(10)]
        [ScaffoldColumn(false)]
        public string LastUpUser { get; set; }

        [Column(TypeName ="varchar",Order = 28),StringLength(10)]
        [ScaffoldColumn(false)]
        public string CreateUser { get; set; }

        [Display(Name ="Öncelik")]
        [Column(Order = 29)]
        public int Priority { get; set; }

        [Display(Name ="Durumu")]
        [Column(Order = 30)]
        public bool IsActive { get; set; }

        public Base()
        {
            LastUpDate = DateTime.Now;
            CreateDate = DateTime.Now;
            IsActive = true;
        }
    }
}
