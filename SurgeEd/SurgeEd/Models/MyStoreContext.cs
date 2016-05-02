using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SurgeEd.Models
{
    public class MyStoreContext: DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .ToTable("Employee")
                .HasKey(e => e.EmployeeId);

            modelBuilder.Entity<Department>()
              .ToTable("Department")
              .HasKey(d => d.DepartmentId);
        }


    }
}