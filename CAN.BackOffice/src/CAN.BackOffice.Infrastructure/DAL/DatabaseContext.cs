using CAN.BackOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAN.BackOffice.Infrastructure.DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Bestelling> Bestellingen { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
