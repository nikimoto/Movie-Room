namespace MoviesRoom.Migrations
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.AspNet.Identity;
    
    internal sealed class Configuration : DbMigrationsConfiguration<MoviesRoom.Data.MoviesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MoviesRoom.Data.MoviesContext context)
        {
            
            context.Roles.AddOrUpdate(
                r => r.Name,
                new Role { Name = "admin" },
                new Role { Name = "user" });
                     
         
            ////  This method will be called after migrating to the latest version.
            ////  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            ////  to avoid creating duplicate seed data. E.g.
            ////
            ////    context.People.AddOrUpdate(
            ////      p => p.FullName,
            ////      new Person { FullName = "Andrew Peters" },
            ////      new Person { FullName = "Brice Lambson" },
            ////      new Person { FullName = "Rowan Miller" }
            ////    );
            ////
        }
    }
}