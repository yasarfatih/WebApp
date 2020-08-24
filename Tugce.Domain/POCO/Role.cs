using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tugce.Domain.POCO
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(20, ErrorMessage = "Rol adı 20 karakterden fazla olamaz.")]
        [Required(ErrorMessage = "Rol adı gereklidir.")]
        public string RoleName { get; set; }

        public virtual ICollection<UserInRole> UserRoles { get; set; }

        public Role()
        {
            UserRoles = new HashSet<UserInRole>();
        }
    }
}
