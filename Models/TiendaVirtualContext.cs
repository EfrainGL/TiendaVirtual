using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace TiendaVirtual.Models
{
    public class TiendaVirtualContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public TiendaVirtualContext() : base("name=TiendaVirtualContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<TiendaVirtual.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<TiendaVirtual.Models.DocumentType> DocumentTypes { get; set; }

        public System.Data.Entity.DbSet<TiendaVirtual.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<TiendaVirtual.Models.Supplier> Suppliers { get; set; }

        public System.Data.Entity.DbSet<TiendaVirtual.Models.Customer> Customers { get; set; }
        public System.Data.Entity.DbSet<TiendaVirtual.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<TiendaVirtual.Models.OrderDetails> OrderDetails { get; set; }
    }
}
