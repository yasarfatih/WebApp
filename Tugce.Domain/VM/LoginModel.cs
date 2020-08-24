using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tugce.Domain.POCO;

namespace Tugce.Domain.VM
{
    public class LoginModel
    {
        [StringLength(10, ErrorMessage = "Kullanıcı adı 10 karakterden fazla olamaz.")]
        [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
        [Display(Name = "Kullanıcı adı")]
        public string UserName { get; set; }

        [StringLength(10, ErrorMessage = "Parola 10 karakterden fazla olamaz.")]
        [Required(ErrorMessage = "Parola gereklidir.")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
