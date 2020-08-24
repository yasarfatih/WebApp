using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tugce.Domain.POCO;
using Tugce.Utils;

namespace Tugce.Domain.VM
{
    public class LoginWithImage
    {
        public LoginUser Login { get; set; }

        public string Password
        {
            get
            {
                return Login.Password;
            }
        }

        [DataType(DataType.Password)]
        [Display(Name = "Parola tekrar")]
        [Compare("Password",ErrorMessage ="Parola ve parola tekrar alanı eşleşmiyor.")]
        public string PasswordAgain { get; set; }

        [Display(Name = "Resim dosyası")]
        [UIHint("ImageUpload")]
        public HttpPostedFileBase PostedFile { get; set; }

        public List<RoleCheckBoxModel> UserRoles { get; set; }

    }

}
