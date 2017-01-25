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
using System.Threading;
using System.Diagnostics;
using CAN.BackOffice.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using System.IO;

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
            var dbconnectionString = "Server=.\\SQLEXPRESS;Database=BackOfficeIntegration;Trusted_Connection=True;";
            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.UseSqlServer(dbconnectionString);
            var dbOptions = builder.Options;
            using (var ctx = new DatabaseContext(dbOptions))
            {
                ctx.Database.ExecuteSqlCommand("Delete from Artikel");
                ctx.Database.ExecuteSqlCommand("Delete from Bestellingen");
                ctx.Database.ExecuteSqlCommand("Delete from Klanten");
                ctx.SaveChanges();
            }

            var directory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\src\\CAN.BackOffice";
            _server = new TestServer(new WebHostBuilder()
                   .UseStartup<TestStartup>()
                   .UseContentRoot(directory)
                   );
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
        public async Task GetFactuurDetailsShowsPage()
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
        public async Task GetFactuurDetailsRedirectsToErrorPage()
        {
            var response = await _client.GetAsync("Factuur/Details/999");

            // Assert
            Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);
            Assert.AreEqual(@"/Factuur/FactuurNietGevonden", response.Headers.Location.ToString());
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _client.Dispose();
            _server.Dispose();
        }
    }
}
