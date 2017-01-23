using CAN.Bestellingbeheer.Domain.DTO;
using CAN.Bestellingbeheer.Domain.Entities;
using CAN.Bestellingbeheer.Domain.Exceptions;
using CAN.Bestellingbeheer.Domain.Interfaces;
using CAN.Bestellingbeheer.Domain.Services;
using InfoSupport.WSA.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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
                        Naam = "Groene batavus",
                        Prijs = 2.50M,
                        Aantal = 1,
                        Artikelnummer = 15224,
                        Leverancier = "Batavus",
                        LeverancierCode = "BTV"
                    }
                },
                VolledigeNaam = "Henk de Vries",
                Postcode = "1233 AA",
                Adres = "Kalvestraat",
                Huisnummer = "31",
                Land = "Nederland"
            };

            var mockPublisher = new Mock<IEventPublisher>();

            var mockRepository = new Mock<IRepository<Bestelling, long>>(MockBehavior.Strict);
            mockRepository.Setup(n => n.Insert(bestelling)).Returns(1);
            mockRepository.Setup(n => n.Dispose());
            var mockLogger = new Mock<ILogger<BestellingService>>(MockBehavior.Loose);

            using (BestellingService service = new BestellingService(mockPublisher.Object, mockRepository.Object, mockLogger.Object))
            {
                //act
                BestellingDTO response = service.CreateBestelling(bestelling);

                //assert
                Assert.IsNotNull(response);
                Assert.IsInstanceOfType(response, typeof(BestellingDTO));

                Assert.IsNotNull(response.Bestellingnummer);
                Assert.AreEqual(bestelling.BestelDatum, response.BestelDatum);
                Assert.AreEqual(bestelling.VolledigeNaam, response.VolledigeNaam);
                Assert.AreEqual(bestelling.Postcode, response.Postcode);
                Assert.AreEqual(bestelling.Adres, response.Adres);
                Assert.AreEqual(bestelling.Huisnummer, response.Huisnummer);
                Assert.AreEqual(bestelling.Land, response.Land);

                Assert.IsNotNull(response.Artikelen.First().Artikelnummer);
                Assert.AreEqual(bestelling.Artikelen.First().Naam, response.Artikelen.First().Naam);
                Assert.AreEqual(bestelling.Artikelen.First().Prijs.ToString(), response.Artikelen.First().Prijs);
                Assert.AreEqual(bestelling.Artikelen.First().Aantal, response.Artikelen.First().Aantal);
                Assert.AreEqual(bestelling.Artikelen.First().Leverancier, response.Artikelen.First().Leverancier);
                Assert.AreEqual(bestelling.Artikelen.First().LeverancierCode, response.Artikelen.First().LeverancierCode);
            };
        }

        [TestMethod]
        public void CreateDoubleBestelling()
        {
            //arrange
            var bestelling = new Bestelling
            {
                Artikelen = new List<Artikel>
                {
                    new Artikel
                    {
                        Naam = "Groene batavus",
                        Prijs = 2.50M,
                        Aantal = 1,
                        Artikelnummer = 15224,
                        Leverancier = "Batavus",
                        LeverancierCode = "BTV"
                    }
                }
            };

            var mockPublisher = new Mock<IEventPublisher>();

            var mockRepository = new Mock<IRepository<Bestelling, long>>(MockBehavior.Strict);
            mockRepository.Setup(n => n.Insert(bestelling)).Returns(1);
            mockRepository.Setup(n => n.Dispose());
            var mockLogger = new Mock<ILogger<BestellingService>>(MockBehavior.Loose);

            using (BestellingService service = new BestellingService(mockPublisher.Object, mockRepository.Object, mockLogger.Object))
            {
                //act
                BestellingDTO response = service.CreateBestelling(bestelling);

                //assert
                Assert.IsNotNull(response);
                Assert.IsInstanceOfType(response, typeof(BestellingDTO));

                Assert.IsNotNull(response.Bestellingnummer);
                Assert.AreEqual(bestelling.BestelDatum, response.BestelDatum);

                Assert.IsNotNull(response.Artikelen.First().Artikelnummer);
                Assert.AreEqual(bestelling.Artikelen.First().Naam, response.Artikelen.First().Naam);
                Assert.AreEqual(bestelling.Artikelen.First().Prijs.ToString(), response.Artikelen.First().Prijs);
                Assert.AreEqual(bestelling.Artikelen.First().Aantal, response.Artikelen.First().Aantal);
                Assert.AreEqual(bestelling.Artikelen.First().Leverancier, response.Artikelen.First().Leverancier);
                Assert.AreEqual(bestelling.Artikelen.First().LeverancierCode, response.Artikelen.First().LeverancierCode);
            };
        }
        [TestMethod]
        public void InvalidStatusExceptionTest()
        {
            //Arrange
            var bestelling = new Bestelling
            {
                Status = BestelStatus.Opgehaald,
                Artikelen = new List<Artikel>
                {
                    new Artikel
                    {
                        Naam = "Groene batavus",
                        Prijs = 2.50M,
                        Aantal = 1,
                        Artikelnummer = 15224,
                        Leverancier = "Batavus",
                        LeverancierCode = "BTV"
                    }
                }
            };
            var mockPublisher = new Mock<IEventPublisher>();
            var mockRepository = new Mock<IRepository<Bestelling, long>>(MockBehavior.Strict);
            mockRepository.Setup(n => n.Find(It.IsAny<long>())).Returns(bestelling);
            mockRepository.Setup(n => n.Dispose());
            var mockLogger = new Mock<ILogger<BestellingService>>(MockBehavior.Loose);

            using (BestellingService service = new BestellingService(mockPublisher.Object, mockRepository.Object, mockLogger.Object))
            {

                Action a = () => service.StatusNaarOpgehaald(1);
                //Act
                Assert.ThrowsException<InvalidBestelStatusException>(a);
            }

            //Assert
            
        }
    }
}
