using CAN.Klantbeheer.Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAN.Klantbeheer.Infrastructure.DAL
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<Klant> Klanten { get; set; }

        public DatabaseContext()
        {
            Database.Migrate();
        }

        public DatabaseContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Klant>().Property(p => p.Klantnummer).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            if (!optionsBuilder.IsConfigured) 
            {
                optionsBuilder.UseSqlServer(@"Server=db;Database=GameServer;UserID=sa,Password=admin");
            }
        }
    }
}