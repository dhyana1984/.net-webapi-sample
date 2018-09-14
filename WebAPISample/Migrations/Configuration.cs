
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    namespace WebAPISample.Migrations
{
   
    internal sealed class Configuration : DbMigrationsConfiguration<WebAPISample.Models.ODataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebAPISample.Models.ODataContext context)
        {
                context.Products.AddOrUpdate(new WebAPISample.Models.Product[] {
                new WebAPISample.Models.Product() { Id = 1, Name = "Hat", Price = 15, Category = "Apparel" },
                new WebAPISample.Models.Product() { Id = 2, Name = "Socks", Price = 5, Category = "Apparel" },
                new WebAPISample.Models.Product() { Id = 3, Name = "Scarf", Price = 12, Category = "Apparel" },
                new WebAPISample.Models.Product() { Id = 4, Name = "Yo-yo", Price = 4.95M, Category = "Toys" },
                new WebAPISample.Models.Product() { Id = 5, Name = "Puzzle", Price = 8, Category = "Toys" },
            });
        }
    }
}
