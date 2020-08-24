namespace Tugce.DataContext.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Tugce.Domain.POCO;

    internal sealed class Configuration : DbMigrationsConfiguration<Tugce.DataContext.TugceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Tugce.DataContext.TugceContext";
        }

        protected override void Seed(Tugce.DataContext.TugceContext context)
        {
            context.Logins.AddOrUpdate(u => u.UserName,
                new LoginUser { Name="Ad1",Surname="Soyad1",UserName="admin",Password="123456"},
                new LoginUser { Name = "Ad2", Surname = "Soyad2", UserName = "user", Password = "123456" }
            );

            context.Roles.AddOrUpdate(r=>r.RoleName,
                new Role { RoleName="admin"},
                new Role { RoleName="user"}
            );

            context.UserRoles.AddOrUpdate(ur => new { ur.RoleId, ur.LoginId },
                new UserInRole { LoginId=1,RoleId=1},
                new UserInRole { LoginId=1,RoleId=2},
                new UserInRole { LoginId=2,RoleId=2}
            );

            context.Categories.AddOrUpdate(c => c.CategoryName,
                new Category { CategoryName = "Kategori 1",Description="Açıklama 1",CreateUser="user-1",LastUpUser="user-1", IsActive = true },
                new Category { CategoryName = "Kategori 2", Description = "Açıklama 2", CreateUser = "user-1", LastUpUser = "user-1", IsActive = true },
                new Category { CategoryName = "Kategori 3", Description = "Açıklama 3", CreateUser = "user-1", LastUpUser = "user-1", IsActive = true });

            context.SaveChanges();
        }
    }
}
