using CAN.Bestellingbeheer.Domain.Entities;
using CAN.Bestellingbeheer.Domain.Exceptions;
using CAN.Bestellingbeheer.Infrastructure.Interfaces;
using CAN.Bestellingbeheer.Infrastructure.Services;
using InfoSupport.WSA.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CAN.Bestellingbeheer.Domain.Test
{
    [TestClass]
    public class BestellingServiceTest
    { 
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
            mockRepository.Setup(n => n.FindBy(It.IsAny<Expression<Func<Bestelling, bool>>>()))
                .Returns(new List<Bestelling>() { bestelling });
            mockRepository.Setup(n => n.Update(It.IsAny<Bestelling>()));
            mockRepository.Setup(n => n.Dispose());
            var mockLogger = new Mock<ILogger<BestellingService>>(MockBehavior.Loose);
             
            using (BestellingService service = new BestellingService(mockPublisher.Object, mockRepository.Object, mockLogger.Object))
            {
                //Act
                Action a = () => service.StatusNaarOpgehaald(1);

                //Assert
                Assert.ThrowsException<InvalidBestelStatusException>(a);
            }
        }
    }
}
