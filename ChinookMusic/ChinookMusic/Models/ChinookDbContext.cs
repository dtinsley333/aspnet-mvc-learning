using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace ChinookMusic.Models
{
    public class ChinookDbContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Album> Album { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
            .ToTable("Customer")
            .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Artist>()
            .ToTable("Artist")
            .HasKey(a => a.ArtistId);

            modelBuilder.Entity<Album>()
           .ToTable("Album")
           .HasKey(a => a.AlbumId);
        }
    }
}
