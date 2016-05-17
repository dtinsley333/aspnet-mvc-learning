using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ChinookMusic3.Models
{
    public class ChinookContext: DbContext
    {
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>()
                .ToTable("Artist")
                .HasKey(c => c.ArtistId);

            modelBuilder.Entity<Genre>()
                .ToTable("Genre")
                .HasKey(c => c.GenreId);

            modelBuilder.Entity<Album>()
              .ToTable("Album")
              .HasKey(c => c.AlbumId);

            modelBuilder.Entity<Customer>()
            .ToTable("Customer")
            .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Invoice>()
                 .ToTable("Invoice")
            .HasKey(c => c.InvoiceId);

        }


    }
}