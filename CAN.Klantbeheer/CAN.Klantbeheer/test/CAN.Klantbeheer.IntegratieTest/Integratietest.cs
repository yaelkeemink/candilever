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
using CAN.Klantbeheer.Domain.Enums;

namespace CAN.Klantbeheer.IntegratieTest
{
    [TestClass]
    public class Integratietest
    {
        [TestMethod]
        public async Task TestAddZonderVoornaam()
        {
            // Arrange
            var _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
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

        [TestMethod]
        public async Task TestAddZonderAchternaam()
        {
            // Arrange
            var _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            var _client = _server.CreateClient();

            var klant = new Klant
            {
                Voornaam = "Yael",
                Tussenvoegsels = "de",
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
        
        [TestMethod]
        public async Task TestAddZonderEmailEnTelefoonnummer()
        {
            // Arrange
            var _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            var _client = _server.CreateClient();

            var klant = new Klant
            {
                Voornaam = "Yael",
                Tussenvoegsels = "de",
                Achternaam = "Keemink",
                Postcode = "2361VJ",
                Adres = "2361VJ",
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
