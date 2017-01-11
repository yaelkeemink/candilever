using CAN.Bestellingbeheer.Domain.Entities;
using CAN.Bestellingbeheer.Domain.Interfaces;
using CAN.Bestellingbeheer.Domain.Services;
using InfoSupport.WSA.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace CAN.Bestellingbeheer.Domain.Test
{
    [TestClass]
    public class BestellingServiceTest
    {
        [TestMethod]
        public void CreateBestelling()
        {
            //arrange
            var mockBestelling = new Bestelling
            {
                Artikelen = new List<Artikel>
                {
                    new Artikel
                    {
                        Naam = "Mijn artikel",
                        Prijs = 2.50M,
                        Aantal = 1
                    }
                }
            };

            var mockLogger = new Mock<ILogger<BestellingService>>();
            var mockPublisher = new Mock<IEventPublisher>();

            var mockRepository = new Mock<IRepository<Bestelling, long>>();
            mockRepository.Setup(n => n.Insert(mockBestelling)).Returns(1);
            
            BestellingService service = new BestellingService(mockLogger.Object, mockPublisher.Object, mockRepository.Object);

            //act
            Bestelling response = service.CreateBestelling(mockBestelling);

            //assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(Bestelling));

            Assert.IsNotNull(response.Id);
            Assert.AreEqual(mockBestelling.BestelDatum, response.BestelDatum);

            Assert.IsNotNull(response.Artikelen.First().Id);
            Assert.AreEqual(mockBestelling.Artikelen.First().Naam, response.Artikelen.First().Naam);
            Assert.AreEqual(mockBestelling.Artikelen.First().Prijs, response.Artikelen.First().Prijs);
            Assert.AreEqual(mockBestelling.Artikelen.First().Aantal, response.Artikelen.First().Aantal);
        }
    }
}
