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


            base.OnModelCreating(modelBuilder);
        }

       internal void PurgeCachedData()
       {
            Database.ExecuteSqlCommand("Delete from Artikel");
            Database.ExecuteSqlCommand("Delete from Bestellingen");
            Database.ExecuteSqlCommand("Delete from Klanten");
        }
    }
}
