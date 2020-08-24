using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tugce.Domain.POCO
{
    [Table("LogErrors")]
    public class LogError:Base
    {
        [Column(TypeName = "varchar",Order =2), StringLength(20)]
        public string Controller { get; set; }

        [Column(TypeName = "varchar",Order =3), StringLength(20)]
        public string Action { get; set; }

        [Column(TypeName = "varchar",Order =4)]
        public string StackTrace { get; set; }

        [Column(TypeName = "varchar",Order =5)]
        public string Message { get; set; }

        [Column(Order = 6)]
        public bool IsAjaxRequest { get; set; }
    }
}
