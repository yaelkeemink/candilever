using CAN.BackOffice.Controllers;
using CAN.BackOffice.Domain.Entities;
using CAN.BackOffice.Domain.Interfaces;
using CAN.BackOffice.Models;
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
    public class FactuurControllerTest
    {

        [TestMethod]
        public void FactuurControllerShowsDetailPage()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<FactuurController>>(MockBehavior.Loose);

            var serviceMock = new Mock<IFactuurService>(MockBehavior.Strict);
            serviceMock.Setup(s => s.ZoekBestelling(1))
                .Returns(new Bestelling()
                {
                    Klantnummer = 12,
                    VolledigeNaam = "Yael Keemink",
                    Adres = "Van leydenstraat",
                    Woonplaats = "Warmont",
                    Huisnummer = "14",
                    Postcode = "2361VJ",
                    Bestellingsnummer = 1,
                    Land = "Nederland",
                    BestelDatum = new DateTime(2017, 1, 24)
                }
            );

            FactuurController target = new FactuurController(loggerMock.Object, serviceMock.Object);

            // Act 
            var response = target.Details(1);

            // Assert
            serviceMock.Verify(s => s.ZoekBestelling(1), Times.Once);

            Assert.IsInstanceOfType(response, typeof(ViewResult));
            var view = response as ViewResult;

            Assert.IsInstanceOfType(view.Model, typeof(FactuurViewModel));
            var model = view.Model as FactuurViewModel;

            Assert.AreEqual(12, model.Bestelling.Klantnummer);
            Assert.AreEqual("Yael Keemink", model.Bestelling.VolledigeNaam);
            Assert.AreEqual("Van leydenstraat", model.Bestelling.Adres);
            Assert.AreEqual("14", model.Bestelling.Huisnummer);
            Assert.AreEqual("Warmont", model.Bestelling.Woonplaats);
            Assert.AreEqual("2361VJ", model.Bestelling.Postcode);
            Assert.AreEqual(1, model.Bestelling.Bestellingsnummer);
            Assert.AreEqual("Nederland", model.Bestelling.Land);
            Assert.AreEqual("24-1-2017 00:00:00", model.Bestelling.BestelDatum.ToString());
            Assert.AreEqual(0, model.Bestelling.Artikelen.Count);
        }

        [TestMethod]
        public void FactuurControllerRedirectsWithInvalidOperationException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<FactuurController>>(MockBehavior.Loose);

            var serviceMock = new Mock<IFactuurService>(MockBehavior.Strict);
            serviceMock.Setup(s => s.ZoekBestelling(It.IsAny<long>()))
                .Throws(new InvalidOperationException());

            FactuurController target = new FactuurController(loggerMock.Object, serviceMock.Object);

            // Act 
            var response = target.Details(5);

            // Assert
            Assert.IsInstanceOfType(response, typeof(RedirectToActionResult));
            var view = response as RedirectToActionResult;

            Assert.AreEqual("FactuurNietGevonden",view.ActionName);
        }

        [TestMethod]
        public void FactuurControllerRedirectsWithAnyOtherException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<FactuurController>>(MockBehavior.Loose);

            var serviceMock = new Mock<IFactuurService>(MockBehavior.Strict);
            serviceMock.Setup(s => s.ZoekBestelling(It.IsAny<long>()))
                .Throws(new Exception());

            FactuurController target = new FactuurController(loggerMock.Object, serviceMock.Object);

            // Act 
            var response = target.Details(5);

            // Assert
            Assert.IsInstanceOfType(response, typeof(RedirectToActionResult));
            var view = response as RedirectToActionResult;

            Assert.AreEqual("Error", view.ActionName);
        }

        [TestMethod]
        public void FactuurControllerGeenFactuurGevondenReturnsViewResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<FactuurController>>(MockBehavior.Loose);
            var serviceMock = new Mock<IFactuurService>(MockBehavior.Loose);

            FactuurController target = new FactuurController(loggerMock.Object, serviceMock.Object);

            // Act 
            var response = target.FactuurNietGevonden();

            // Assert
            Assert.IsInstanceOfType(response, typeof(ViewResult));
        }
    }
}
