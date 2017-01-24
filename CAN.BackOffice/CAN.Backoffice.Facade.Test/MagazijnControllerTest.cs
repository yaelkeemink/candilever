using CAN.BackOffice.Controllers;
using CAN.BackOffice.Domain.Entities;
using CAN.BackOffice.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Backoffice.Facade.Test
{
    [TestClass]
    public class MagazijnControllerTest
    {
        [TestMethod]
        public void MagazijnBestellingOphalen()
        {
            var bestelling = new Bestelling()
            {
                Klantnummer = 12,
                VolledigeNaam = "Yael Keemink",
                Adres = "Van leydenstraat",
                Huisnummer = "14",
                Postcode = "2361VJ",
                Bestellingsnummer = 1,
                Land = "Nederland",
            };
            var loggerLock = new Mock<ILogger>(MockBehavior.Loose);
            var serviceMock = new Mock<IMagazijnService>(MockBehavior.Strict);
            serviceMock.Setup(s => s.GetVolgendeBestelling())
                .Returns(bestelling);            

            var target = new MagazijnController(loggerLock.Object, serviceMock.Object);

            // Act

            var result = target.BestellingOphalen();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var view = result as ViewResult;

            Assert.IsInstanceOfType(view.Model, typeof(Bestelling));
            var model = view.Model as Bestelling;

            Assert.AreEqual(12, model.Klantnummer);
            Assert.AreEqual(1, model.Bestellingsnummer);
            Assert.AreEqual("Yael Keemink", model.VolledigeNaam);
            Assert.AreEqual("Van leydenstraat", model.Adres);
            Assert.AreEqual("14", model.Huisnummer);
            Assert.AreEqual("Nederland", model.Land);
            Assert.AreEqual("2361VJ", model.Postcode);
        }
    }
}
