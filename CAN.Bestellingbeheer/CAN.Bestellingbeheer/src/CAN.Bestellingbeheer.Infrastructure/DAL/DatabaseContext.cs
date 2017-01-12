using CAN.Bestellingbeheer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAN.Bestellingbeheer.Infrastructure.DAL
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<Bestelling> Bestellingen { get; set; }

        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        public DatabaseContext(DbContextOptions options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bestelling>().HasKey(e => e.Bestellingnummer);
            modelBuilder.Entity<Artikel>().HasKey(e => e.Artikelnummer);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {

        }
    }
}