using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net;
using InfoSupport.WSA.Infrastructure;
using CAN.Common.Events;

namespace CAN.BackOffice.IntegrationTest
{
    [TestClass]
    public class IntegrationTest
    {
        private static TestServer _server;
        private static HttpClient _client;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _server = new TestServer(new WebHostBuilder()
                    .UseStartup<TestStartup>());
            _client = _server.CreateClient();

            using (var publisher = new EventPublisher(new BusOptions()
            {
                ExchangeName = "TestExchange",
                QueueName = null,
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
            }))
            {


                KlantCreatedEvent ev1 = new KlantCreatedEvent("CAN.BackOffice.IntegrationTest")
                {
                    Voornaam = "Yael",
                    Tussenvoegsels = "De",
                    Achternaam = "Keemink",
                    Postcode = "2361VJ",
                    Adres = "van Leydenstraat",
                    Huisnummer = "14",
                    Email = "yaelkeemink@gmail.com",
                    Telefoonnummer = "+31640480381",
                    Land = "Nederland",
                    Klantnummer = 12
                };

                publisher.Publish(ev1);

                BestellingCreatedEvent ev2 = new BestellingCreatedEvent("CAN.BackOffice.IntegrationTest")
                {
                    Klantnummer = 12,
                    VolledigeNaam = "Yael Keemink",
                    Adres = "Van leydenstraat",
                    //Woonplaats = "Warmont",
                    Huisnummer = "14",
                    Postcode = "2361VJ",
                    Bestellingsnummer = 1,
                    Land = "Nederland",
                    BestelDatum = new DateTime(2017, 1, 24),
                    BestellingStatusCode = "Goedgekeurd"
                };
                ev2.AddArtikel(1, "Mountain Bike", 789M, 1, "IS-1234", "InfoSupport");
                ev2.AddArtikel(2, "Fietslicht", 3M, 2, "IS-1235", "InfoSupport");

                publisher.Publish(ev2);
            }
        }

        [TestMethod]
        public async Task GetFactuurDetails()
        {

            // Arrange
            //var json = JsonConvert.SerializeObject(null);

            // Act
            //var response = await _client.PostAsync("api/klant", new StringContent(json, Encoding.UTF8, "application/json"));
            var response = await _client.GetAsync("Factuur/Details/1");


            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void MyTestMethod()
        {

        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _client.Dispose();
            _server.Dispose();
        }
    }
}
