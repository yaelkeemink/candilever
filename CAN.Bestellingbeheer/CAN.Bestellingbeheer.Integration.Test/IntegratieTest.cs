using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using CAN.Bestellingbeheer.Facade;
using CAN.Bestellingbeheer.Domain.Entities;

namespace CAN.Bestellingbeheer.Integration.Test
{
    [TestClass]
    public class IntegratieTest
    {

        [TestMethod]
        public async Task TestBestellingStatusOphalen()
        {
            // Arrange
            var _server = new TestServer(new WebHostBuilder()
                .UseStartup<TestStartup>());
            var _client = _server.CreateClient();

            var bestelling = 5;

            var json = JsonConvert.SerializeObject(bestelling);

            // Act
            var response = await _client.PutAsync("api/bestelling", new StringContent(json, Encoding.UTF8, "application/json"));
            
            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
