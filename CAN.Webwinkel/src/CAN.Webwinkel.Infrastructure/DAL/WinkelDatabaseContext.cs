﻿using CAN.Webwinkel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAN.Webwinkel.Infrastructure.DAL
{
    public class WinkelDatabaseContext : DbContext
    {
        public virtual DbSet<Artikel> Artikels { get; set; }
        public virtual DbSet<Categorie> Categorieen { get; set; }
        public virtual DbSet<Winkelmandje> Winkelmandjes { get; set; }
        public WinkelDatabaseContext()
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public WinkelDatabaseContext(DbContextOptions options)
            : base(options)
        {

            Database.EnsureCreated();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ArtikelCategorie>().HasKey(x => new { x.ArtikelId, x.CategoryId });


            modelBuilder.Entity<ArtikelCategorie>()
             .HasOne(a => a.Artikel)
             .WithMany(ac => ac.ArtikelCategorie)
             .HasForeignKey(ac => ac.ArtikelId);

            modelBuilder.Entity<ArtikelCategorie>()
                .HasOne(c => c.Categorie)
                .WithMany(ac => ac.ArtikelCategorie)
                .HasForeignKey(ac => ac.CategoryId);

            modelBuilder.Entity<Categorie>().HasAlternateKey(c => c.Naam).HasName("AlternateKey_CategoryName");
        }

        /// <summary>
        /// 
        /// </summary>
        internal void PurgeCachedData()
        {
            Database.ExecuteSqlCommand("Delete from ArtikelCategorie");
            Database.ExecuteSqlCommand("Delete from Artikels");
            Database.ExecuteSqlCommand("Delete from Categorieen");
        }
    }
}