using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using CAN.Klantbeheer.Facade;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace CAN.Klantbeheer.IntegratieTest
{
    [TestClass]
    public class Integratietest
    {
        //[TestMethod]
        //Werkt nog niet!
        public async void TestAddZonderVoornaam()
        {
            // Arrange
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            var client = server.CreateClient();

            // Act
            var response = await client.GetAsync("api/v1/Klant");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.AreEqual("[{\"id\":1,\"hoogte\":300,\"naam\":\"Eiffeltoren\"},{\"id\":2,\"hoogte\":56,\"naam\":\"Toren van Pisa\"},{\"id\":3,\"hoogte\":381,\"naam\":\"Empire State Building\"}]", responseString);
        }
    }
}
