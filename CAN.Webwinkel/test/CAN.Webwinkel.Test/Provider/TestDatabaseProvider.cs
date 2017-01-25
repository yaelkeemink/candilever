using CAN.Webwinkel.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CAN.Webwinkel.Test
{
    public static class TestDatabaseProvider
    {
        public static DbContextOptions<WinkelDatabaseContext> CreateInMemoryDatabaseOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<WinkelDatabaseContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }


        public static DbContextOptions<WinkelDatabaseContext> CreateMsSQLDatabaseOptions()
        {
            var builder = new DbContextOptionsBuilder<WinkelDatabaseContext>();
           
            builder.UseSqlServer("Server=.\\SQLEXPRESS; Database=Webshop; Trusted_Connection=True;");
            return builder.Options;
        }
    }
}
