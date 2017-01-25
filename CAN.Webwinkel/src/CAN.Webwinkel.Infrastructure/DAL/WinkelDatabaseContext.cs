using CAN.Webwinkel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAN.Webwinkel.Infrastructure.DAL
{
    public class WinkelDatabaseContext : DbContext
    {
        public virtual DbSet<Artikel> Artikels { get; set; }
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
        }

        /// <summary>
        /// 
        /// </summary>
        internal void PurgeCachedData()
        {
            Database.ExecuteSqlCommand("Delete from Artikels");
        }
    }
}