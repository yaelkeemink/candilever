using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Threading;

namespace CAN.Bestellingbeheer.Facade.Facade
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Thread.Sleep(60000);

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()                
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
