using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tugce.Domain.POCO;

namespace Tugce.DataContext
{
    public class TugceContext:DbContext
    {
        public TugceContext():base("TugceConnection")
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<LogError> Errors { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<SiteStatic> Statics { get; set; }

        public DbSet<LoginUser> Logins { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserInRole> UserRoles { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}
