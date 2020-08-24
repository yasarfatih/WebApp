using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tugce.Domain.POCO
{
    [Table("Messages")]
    public class Message
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int Id { get; set; }

        [Column(TypeName = "varchar", Order = 2)]
        [StringLength(50, ErrorMessage = "Kategori adı en fazla 50 karakter olabilir.")]
        [Required(ErrorMessage = "Ad soyad girilmelidir.")]
        [Display(Name = "Adınız")]
        public string From { get; set; }

        [Column(TypeName = "varchar", Order = 3)]
        [StringLength(100, ErrorMessage = "Mesaj konusu en fazla 100 karakter olabilir.")]
        [Required(ErrorMessage = "Mesaj konusu boş bırakılamaz.")]
        [Display(Name = "Konu")]
        public string Subject { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Email adresi uygun formatta değil.")]
        [Required(ErrorMessage = "Email adresi girilmelidir.")]
        [Column(TypeName = "varchar", Order = 4)]
        [StringLength(100, ErrorMessage = "Email adresi 100 karakteri geçemez.")]
        [Display(Name = "Eposta adresi")]
        public string Email { get; set; }

        [Column(TypeName = "varchar", Order = 5)]
        [StringLength(255, ErrorMessage = "Mesaj içeriği en fazla 255 karakter olabilir.")]
        [Required(ErrorMessage = "Mesaj içeriği boş bırakılamaz.")]
        [Display(Name = "Mesajınız")]
        public string Body { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; }

        [ScaffoldColumn(false)]
        public bool IsActive { get; set; }

        public Message()
        {
            CreateDate = DateTime.Now;
            IsActive = true;
        }
    }
}
