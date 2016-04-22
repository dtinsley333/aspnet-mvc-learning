using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BasicEntityFrameworkDataAccess.Models
{
    public class MyStoreContext: DbContext
    {
    
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .ToTable("Customer")
                .HasKey(c => c.CustomerId);




            modelBuilder.Entity<Employee>()
                .ToTable("Employee")
                .HasKey(c => c.EmployeeId);
        }


    }
}