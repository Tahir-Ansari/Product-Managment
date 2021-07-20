namespace ProductManagement.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductManagement.DAL.ProductManageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProductManagement.DAL.ProductManageContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Categories.AddOrUpdate(
               x => x.ID,
                //new Models.Category { ID = 1, Name = "Computer", CreatedOn = DateTime.Now },
                //new Models.Category { ID = 2, Name = "SpareParts", CreatedOn = DateTime.Now }
                new Models.Category { ID = 1, Name = "Cloths", CreatedOn = DateTime.Now },
                new Models.Category { ID = 2, Name = "Electronics", CreatedOn = DateTime.Now },
                new Models.Category { ID = 3, Name = "Grocerry", CreatedOn = DateTime.Now }
               );

            context.SaveChanges();
        }
    }
}
