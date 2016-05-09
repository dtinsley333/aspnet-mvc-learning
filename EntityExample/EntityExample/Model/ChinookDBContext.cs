using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EntityExample.Model
{
    public class ChinookDBContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
            .ToTable("Customer")
            .HasKey(c => c.CustomerId);
        }

    }
}
