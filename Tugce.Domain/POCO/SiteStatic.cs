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
    [Table("Statics")]
    public class SiteStatic : Base
    {
        [Required(ErrorMessage = "Hakkımızda sayfasının başlığı girilmelidir.")]
        [Column(TypeName = "varchar",Order =2)]
        [StringLength(100, ErrorMessage = "Hakkımızda sayfasının başlığı 100 karakteri geçemez.")]
        [Display(Name ="Hakkımızda sayfasının başlığı")]
        public string AboutTitle { get; set; }

        [Required(ErrorMessage = "Hakkımızda sayfasının içeriği girilmelidir.")]
        [Column(TypeName = "varchar",Order =3)]
        [StringLength(8000, ErrorMessage = "Hakkımızda sayfasının içeriği 8000 karakteri geçemez.")]
        [AllowHtml]
        [Display(Name = "Hakkımızda sayfasının içeriği")]
        public string AboutContent { get; set; }

        [Column(TypeName = "varchar",Order =4)]
        [Required(ErrorMessage = "Footer metni girilmelidir.")]
        [StringLength(255, ErrorMessage = "Footer metni 255 karakteri geçemez")]
        [Display(Name = "Copyright metni")]
        public string Footer { get; set; }

        [Column(TypeName = "varchar", Order = 5)]
        [Required(ErrorMessage = "Firma adı girilmelidir.")]
        [StringLength(100, ErrorMessage = "Firma adı 100 karakteri geçemez")]
        [Display(Name = "Firma adı")]
        public string Firma { get; set; }

        [Column(TypeName = "varchar",Order =6)]
        [StringLength(50)]
        public string VideoFile { get; set; }

        [Column(TypeName = "varchar", Order = 7)]
        [StringLength(50)]
        public string LogoFile { get; set; }

        [Column(TypeName = "varchar", Order = 8)]
        [StringLength(50)]
        public string SloganTitle { get; set; }

        [Required(ErrorMessage = "Telefon no 1 girilmelidir.")]
        [Column(TypeName = "varchar",Order =9)]
        [StringLength(15, ErrorMessage = "Telefon numarası1 15 karakteri geçemez.")]
        [RegularExpression(@"^\((\d{3}\)) (\d{3}) (\d{2}) (\d{2})$", ErrorMessage = "Telefon numarası1 (xxx)xxx xx xx şablonuna uygun olmalıdır.")]
        [Display(Name = "Telefon no-1")]
        public string Phone1 { get; set; }

        [Column(TypeName = "varchar",Order =10)]
        [StringLength(15, ErrorMessage = "Telefon numarası2 15 karakteri geçemez.")]
        [RegularExpression(@"^\((\d{3}\)) (\d{3}) (\d{2}) (\d{2})$", ErrorMessage = "Telefon numarası2 (xxx)xxx xx xx şablonuna uygun olmalıdır.")]
        [Display(Name = "Telefon no-2")]
        public string Phone2 { get; set; }

        [Required(ErrorMessage = "Cep telefonu1 girilmelidir.")]
        [Column(TypeName = "varchar",Order =11)]
        [StringLength(15, ErrorMessage = "Cep telefonu1 15 karakteri geçemez.")]
        [RegularExpression(@"^\((\d{3}\)) (\d{3}) (\d{2}) (\d{2})$", ErrorMessage = "Cep telefonu1 (xxx)xxx xx xx şablonuna uygun olmalıdır.")]
        [Display(Name = "Cep telefon no-1")]
        public string Mobile1 { get; set; }

        [Column(TypeName = "varchar",Order =12)]
        [StringLength(15, ErrorMessage = "Cep telefonu2 15 karakteri geçemez.")]
        [RegularExpression(@"^\((\d{3}\)) (\d{3}) (\d{2}) (\d{2})$", ErrorMessage = "Cep telefonu2 (xxx)xxx xx xx şablonuna uygun olmalıdır.")]
        [Display(Name = "Cep telefon no-2")]
        public string Mobile2 { get; set; }

        [Column(TypeName = "varchar", Order = 13)]
        [StringLength(255, ErrorMessage = "Adres bilgisi 255 karakteri geçemez.")]
        [Display(Name = "Adres")]
        [Required(ErrorMessage = "Adres alanı boş bırakılamaz.")]
        public string Address { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Email adresi uygun formatta değil.")]
        [Required(ErrorMessage = "Email adresi girilmelidir.")]
        [Column(TypeName = "varchar",Order =14)]
        [StringLength(100,ErrorMessage ="Email adresi 100 karakteri geçemez.")]
        [Display(Name = "Eposta adresi")]
        public string Email { get; set; }

        [DataType(DataType.Url, ErrorMessage = "Facebook adresi uygun formatta değil.")]
        [Required(ErrorMessage = "Facebook adresi girilmelidir.")]
        [Column(TypeName = "varchar",Order =15)]
        [StringLength(100, ErrorMessage = "Facebook adresi 100 karakteri geçemez.")]
        [Display(Name = "Facebook adresi")]
        public string Facebook { get; set; }

        [DataType(DataType.Url, ErrorMessage = "Twitter adresi uygun formatta değil.")]
        [Required(ErrorMessage = "Twitter adresi girilmelidir.")]
        [Column(TypeName = "varchar",Order =16)]
        [StringLength(100, ErrorMessage = "Twitter adresi 100 karakteri geçemez.")]
        [Display(Name = "Twitter adresi")]
        public string Twitter { get; set; }

        [DataType(DataType.Url, ErrorMessage = "LinkedIn adresi uygun formatta değil.")]
        [Required(ErrorMessage = "LinkedIn adresi girilmelidir.")]
        [Column(TypeName = "varchar",Order =17)]
        [StringLength(100, ErrorMessage = "LinkedIn adresi 100 karakteri geçemez.")]
        [Display(Name = "LinkedIn adresi")]
        public string LinkedIn { get; set; }

        [DataType(DataType.Url, ErrorMessage = "Pinterest adresi uygun formatta değil.")]
        [Required(ErrorMessage = "Pinterest adresi girilmelidir.")]
        [Column(TypeName = "varchar",Order =18)]
        [StringLength(100, ErrorMessage = "Pinterest adresi 100 karakteri geçemez.")]
        [Display(Name = "Pinterest adresi")]
        public string Pinterest { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Email adresi uygun formatta değil.")]
        [Required(ErrorMessage = "Admin email adresi girilmelidir.")]
        [Column(TypeName = "varchar", Order = 19)]
        [StringLength(100, ErrorMessage = "Admin email adresi 100 karakteri geçemez.")]
        [Display(Name = "Admin eposta adresi")]
        public string AdminEmail { get; set; }

        [Required(ErrorMessage = "Admin eposta kullanıcı adı.")]
        [Column(TypeName = "varchar", Order = 20)]
        [StringLength(10, ErrorMessage = "Admin eposta kullanıcı adı 10 karakteri geçemez.")]
        [Display(Name = "Admin eposta kullanıcı adı")]
        public string AdminEmailUser { get; set; }

        [Required(ErrorMessage = "Admin eposta parolası gereklidir.")]
        [Column(TypeName = "varchar", Order = 21)]
        [StringLength(10, ErrorMessage = "Admin eposta parolası 10 karakteri geçemez.")]
        [Display(Name = "Admin eposta parolası")]
        public string AdminEmailPassword { get; set; }

        [Required(ErrorMessage = "Konum bilgisi boş bırakılamaz.")]
        [StringLength(800,ErrorMessage ="Konum bilgisi 800 karakteri geçemez.")]
        [Column(Order =22,TypeName ="varchar")]
        [Display(Name = "Konum Etiketi")]
        [AllowHtml]
        public string Location { get; set; }
        
    }
}
