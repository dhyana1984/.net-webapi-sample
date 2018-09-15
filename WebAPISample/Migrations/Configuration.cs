
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
using WebAPISample.Models;

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
                context.Products.AddOrUpdate(new Product[] {
                new Product() { Id = 1, Name = "Hat", Price = 15, Category = "Apparel", SupplierId="00a" },
                new Product() { Id = 2, Name = "Socks", Price = 5, Category = "Apparel",SupplierId="00b" },
                new Product() { Id = 3, Name = "Scarf", Price = 12, Category = "Apparel",SupplierId="00c" },
                new Product() { Id = 4, Name = "Yo-yo", Price = 4.95M, Category = "Toys",SupplierId="00a" },
                new Product() { Id = 5, Name = "Puzzle", Price = 8, Category = "Toys",SupplierId="00b" },
            });

            context.Suppliers.AddOrUpdate(new Supplier[] {
                 new Supplier(){Key="00a",Name="AliCloud"},
                 new Supplier(){Key="00b",Name="TencentCloud"},
                 new Supplier(){Key="00c",Name="BaiduCloud"},
            });
        }
    }
}
