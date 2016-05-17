using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Entity;


namespace AspNETMVC51st.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private string connectionstring { get; set; }
        public ApplicationDbContext(string _connectionString)
        {
            connectionstring = _connectionString;
        }
        public DbSet<Document> Document { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            var connectionString = connectionstring;

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Document>()
               .ToTable("Document")
               .HasKey(e => e.DocumentId);
        }
    }
}
