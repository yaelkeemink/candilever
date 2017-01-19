using CAN.BackOffice.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CAN.BackOffice.Infrastructure.Test.Provider
{
    public static class TestDatabaseProvider
    {
        public static DbContextOptions<DatabaseContext> CreateInMemoryDatabaseOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }


        public static DbContextOptions<DatabaseContext> CreateMsSQLDatabaseOptions()
        {
            var builder = new DbContextOptionsBuilder<DatabaseContext>();
           
            builder.UseSqlServer("Server=.\\SQLEXPRESS; Database=WebshopTest; Trusted_Connection=True;");
            return builder.Options;
        }
    }
}
