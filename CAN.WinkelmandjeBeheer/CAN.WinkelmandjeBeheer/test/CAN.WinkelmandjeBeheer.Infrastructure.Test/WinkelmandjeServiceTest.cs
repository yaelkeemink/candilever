using CAN.WinkelmandjeBeheer.Domain.Domain.Entities;
using CAN.WinkelmandjeBeheer.Domain.Domain.Interfaces;
using CAN.WinkelmandjeBeheer.Domain.Domain.Services;
using CAN.WinkelmandjeBeheer.Domain.DTO;
using InfoSupport.WSA.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.WinkelmandjeBeheer.Infrastructure.Test
{
    [TestClass]
    public class WinkelmandjeServiceTest
    {
        [TestMethod]
        public void TestUpdateWinkelmandjeMetAantalOphogen()
        {
            var guid = Guid.NewGuid().ToString();
            var winkelmandje = new Winkelmandje
            {
                Artikelen = new List<ArtikelDTO>
                {
                    new ArtikelDTO
                    {
                        Naam = "Groene batavus",
                        Prijs = "2.50",
                        Aantal = 1,
                        Artikelnummer = 15224,
                        Leverancier = "Batavus",
                        LeverancierCode = "BTV"
                    }
                },
                WinkelmandjeNummer = guid
            };

            var mockPublisher = new Mock<IEventPublisher>();

            var mockRepository = new Mock<IRepository<Winkelmandje, string>>(MockBehavior.Loose);
            mockRepository.Setup(n => n.Insert(winkelmandje)).Returns(1);
            mockRepository.Setup(n => n.Dispose());
            mockRepository.Setup(n => n.Find(winkelmandje.WinkelmandjeNummer)).Returns(winkelmandje);
            var mockLogger = new Mock<ILogger<WinkelmandjeService>>(MockBehavior.Loose);

            using (var service = new WinkelmandjeService(mockRepository.Object, mockPublisher.Object, mockLogger.Object))
            {
                var mandje = service.CreateWinkelmandje(winkelmandje);

                var updateMandje = service.UpdateWinkelmandje(winkelmandje);

                var dbMnadje = mockRepository.Object.Find(updateMandje);

                Assert.IsNotNull(dbMnadje);
                Assert.AreEqual(1, dbMnadje.Artikelen.Count);
                Assert.AreEqual(2, dbMnadje.Artikelen.First().Aantal);
            }
        }
        [TestMethod]
        public void TestUpdateWinkelmandjeMetNieuwArtikel()
        {
            var guid = Guid.NewGuid().ToString();

            var artikel = new ArtikelDTO
            {
                Naam = "Groene batavus",
                Prijs = "2.50",
                Aantal = 1,
                Artikelnummer = 15224,
                Leverancier = "Batavus",
                LeverancierCode = "BTV"
            };

            var winkelmandje = new Winkelmandje
            {
                Artikelen = new List<ArtikelDTO>
                {
                    artikel
                },
                WinkelmandjeNummer = guid
            };

            var newArtikel = new ArtikelDTO
            {
                Naam = "Blauwe batavus",
                Prijs = "2.50",
                Aantal = 1,
                Artikelnummer = 15224,
                Leverancier = "Batavus",
                LeverancierCode = "BTV"
            };
            var newWinkelmandje = new Winkelmandje
            {
                Artikelen = new List<ArtikelDTO>
                {
                    newArtikel
                },
                WinkelmandjeNummer = guid
            };

            var mockPublisher = new Mock<IEventPublisher>();

            var mockRepository = new Mock<IRepository<Winkelmandje, string>>(MockBehavior.Loose);
            mockRepository.Setup(n => n.Insert(winkelmandje)).Returns(1);
            mockRepository.Setup(n => n.Dispose());
            mockRepository.Setup(n => n.Find(winkelmandje.WinkelmandjeNummer)).Returns(winkelmandje);
            var mockLogger = new Mock<ILogger<WinkelmandjeService>>(MockBehavior.Loose);

            using (var service = new WinkelmandjeService(mockRepository.Object, mockPublisher.Object, mockLogger.Object))
            {
                var mandje = service.CreateWinkelmandje(winkelmandje);

                var updateMandje = service.UpdateWinkelmandje(newWinkelmandje);

                var dbMnadje = mockRepository.Object.Find(updateMandje);

                Assert.IsNotNull(dbMnadje);
                Assert.AreEqual(2, dbMnadje.Artikelen.Count);
                Assert.IsTrue(dbMnadje.Artikelen.Contains(artikel));
                Assert.IsTrue(dbMnadje.Artikelen.Contains(newArtikel));
            }
        }
    }
}
