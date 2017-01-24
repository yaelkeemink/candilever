using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;

namespace CAN.Bestellingbeheer.Integration.Test
{
    [TestClass]
    public class IntegratieTest
    {
        [TestMethod]
        public async Task TestAddZonderVoornaam()
        {
            // Arrange
            var _server = new TestServer(new WebHostBuilder()
                .UseStartup<TestStartup>());
            var _client = _server.CreateClient();

            var klant = new Klant
            {
                Tussenvoegsels = "de",
                Achternaam = "Keemink",
                Postcode = "2361VJ",
                Telefoonnummer = "0640480381",
                Adres = "2361VJ",
                Email = "yaelkeemink@gmail.com",
                Huisnummer = "14",
                Land = Land.Nederland,
            };
            var json = JsonConvert.SerializeObject(klant);

            // Act
            var response = await _client.PostAsync("api/klant", new StringContent(json, Encoding.UTF8, "application/json"));


            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
