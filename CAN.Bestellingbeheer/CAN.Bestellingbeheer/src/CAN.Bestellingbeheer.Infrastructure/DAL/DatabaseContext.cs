using CAN.Bestellingbeheer.Domain.Domain.Entities;
using CAN.Bestellingbeheer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAN.Bestellingbeheer.Infrastructure.DAL
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<Bestelling> Bestellingen { get; set; }

        public DatabaseContext()
        {
            Database.Migrate();
        }

        public DatabaseContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            if (!optionsBuilder.IsConfigured) 
            {
                optionsBuilder.UseSqlServer(@"Server=db;Database=can-kantilever-eventbus;UserID=sa,Password=admin");
            }
        }
    }
}