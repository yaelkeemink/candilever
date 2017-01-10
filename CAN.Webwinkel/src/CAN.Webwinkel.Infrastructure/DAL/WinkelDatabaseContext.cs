using CAN.Webwinkel.Infrastructure.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAN.Webwinkel.Infrastructure.DAL
{
    public class WinkelDatabaseContext : DbContext
    {
        public virtual DbSet<Artikel> Artikels { get; set; }
        public virtual DbSet<Category> Categorieen { get; set; }
        public WinkelDatabaseContext()
        {
            Database.EnsureCreated();
        }

        public WinkelDatabaseContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<ArtikelCategory>().HasKey(x => new { x.ArtikelId, x.CategoryId });
      

            modelBuilder.Entity<ArtikelCategory>()
             .HasOne(a => a.Artikel)
             .WithMany(ac => ac.ArtikelCategory)
             .HasForeignKey(ac => ac.ArtikelId);

            modelBuilder.Entity<ArtikelCategory>()
                .HasOne(c => c.Category)
                .WithMany(ac => ac.ArtikelCategory)
                .HasForeignKey(ac => ac.CategoryId);

            modelBuilder.Entity<Category>().HasAlternateKey(c => c.Naam).HasName("AlternateKey_CategoryName");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}