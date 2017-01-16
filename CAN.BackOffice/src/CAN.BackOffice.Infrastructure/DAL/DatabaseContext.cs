using System;
using Microsoft.EntityFrameworkCore;
using Can.BackOffice.Domain.Entities;
using CAN.BackOffice.Domain.Entities;

namespace CAN.BackOffice.Infrastructure.DAL
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<Bestelling> Bestellingen { get; set; }

        public virtual DbSet<Klant> Klanten { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bestelling>().HasKey(e => e.Bestellingsnummer);
            modelBuilder.Entity<Artikel>().HasKey(e => e.Artikelnummer);
            modelBuilder.Entity<Klant>().HasKey(e => e.Klantnummer);

            base.OnModelCreating(modelBuilder);
        }

       internal void PurgeCachedData()
       {
            Database.ExecuteSqlCommand("Delete from Bestelling");
            Database.ExecuteSqlCommand("Delete from Artikel");
        }
    }
}
