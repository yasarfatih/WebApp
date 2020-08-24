using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tugce.Domain.POCO;

namespace Tugce.Web.Models.VM
{
    public class TopMenuModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public string LogoImage { get; set; }
        public string SloganTitle { get; set; }
        public string Controller { get; set; }
        public string Firma { get; set; }
    }
}