using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//plain old clr object
namespace Tugce.Domain.POCO
{
    [Table("Logins")]
    public class LoginUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName ="varchar")]
        [StringLength(10,ErrorMessage ="Kullanıcı adı 10 karakterden fazla olamaz.")]
        [Required(ErrorMessage ="Kullanıcı adı gereklidir.")]
        [Display(Name="Kullanıcı adı")]
        public string UserName { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(10, ErrorMessage = "Parola 10 karakterden fazla olamaz.")]
        [Required(ErrorMessage = "Parola gereklidir.")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(30, ErrorMessage = "İsim en fazla 30 karakter olabilir.")]
        [Required(ErrorMessage = "İsim girilmelidir.")]
        [Display(Name = "İsim")]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(30, ErrorMessage = "Soyad en fazla 30 karakter olabilir.")]
        [Required(ErrorMessage = "Soyad girilmelidir.")]
        [Display(Name = "Soyisim")]
        public string Surname { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string UserImage { get; set; }

        [Display(Name="Roller")]
        public virtual ICollection<UserInRole> UserRoles { get; set; }

        public LoginUser()
        {
            UserRoles = new HashSet<UserInRole>();
        }
    }
}
