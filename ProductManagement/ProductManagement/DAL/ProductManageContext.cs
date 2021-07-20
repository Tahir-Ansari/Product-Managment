using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductManagement.DAL
{
    public class ProductManageContext:DbContext
    {
        public ProductManageContext() : base("ProductManageContext")
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}