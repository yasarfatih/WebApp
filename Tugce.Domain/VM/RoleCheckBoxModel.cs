using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tugce.Domain.VM
{
    public class RoleCheckBoxModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsChecked { get; set; }
    }
}
