using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using CAN.Klantbeheer.Facade;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using CAN.Klantbeheer.Domain.Entities;

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
            var _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            var _client = _server.CreateClient();

            var klant = new Klant
            {
                Achternaam = "Keemink",
                Tussenvoegsels = "de",
                Postcode = "2361VJ",
                Telefoonnummer = ""
            };
            var json = JsonConvert.SerializeObject(klant);

            // Act
            var response = await _client.PostAsync("api/v1/monumenten", new StringContent(json, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
