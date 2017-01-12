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
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {

        }
    }
}