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
            var bestelling = new Bestelling
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

            var mockPublisher = new Mock<IEventPublisher>();

            var mockRepository = new Mock<IRepository<Bestelling, long>>();
            mockRepository.Setup(n => n.Insert(bestelling)).Returns(1);

            using (BestellingService service = new BestellingService(mockPublisher.Object, mockRepository.Object))
            {
                //act
                Bestelling response = service.CreateBestelling(bestelling);

                //assert
                Assert.IsNotNull(response);
                Assert.IsInstanceOfType(response, typeof(Bestelling));

                Assert.IsNotNull(response.Id);
                Assert.AreEqual(bestelling.BestelDatum, response.BestelDatum);

                Assert.IsNotNull(response.Artikelen.First().Id);
                Assert.AreEqual(bestelling.Artikelen.First().Naam, response.Artikelen.First().Naam);
                Assert.AreEqual(bestelling.Artikelen.First().Prijs, response.Artikelen.First().Prijs);
                Assert.AreEqual(bestelling.Artikelen.First().Aantal, response.Artikelen.First().Aantal);
            };
        }
    }
}
