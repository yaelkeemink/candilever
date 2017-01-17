using CAN.Webwinkel.Controllers;
using CAN.Webwinkel.Domain.Entities;
using CAN.Webwinkel.Domain.Interfaces;
using CAN.Webwinkel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Test
{
    [TestClass]
    public class ArtikelControllerTest
    {
        [TestMethod]
        public void ArtikelController_instantie()
        {
            // Arrange
            ArtikelController target = new ArtikelController(null, null);

            // Act

            // Assert
            Assert.IsNotNull(target);
        }

        [TestMethod]
        public void ArtikelController_GetListArtikelen()
        {
            // Arrange
            var categorieNaam = "sloten";

            var loggerMock = new Mock<ILogger<ArtikelController>>(MockBehavior.Loose);

            var serviceMock = new Mock<IArtikelService>(MockBehavior.Strict);
            serviceMock.Setup(s => s.ArtikelenBijCategorie(categorieNaam))
                .Returns(new List<Artikel>() {
                    new Artikel {
                        Artikelnummer = 5,
                        Beschrijving = "Klein fiets slot",
                        AfbeeldingUrl = "bike_lock_small.gif",
                        Leverancier = "Unilever",
                        LeverancierCode = "74",
                        Prijs = 28.32M,
                        Naam = "Slot",
                        Voorraad = 19,
                        LeverbaarTot = DateTime.ParseExact("2017-12-31 00:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                        LeverbaarVanaf = DateTime.ParseExact("2017-01-01 00:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                        ArtikelCategorie = null,
                        Id = 2
                    }
                });

            serviceMock.Setup(s => s.AlleArtikelen())
                .Returns(new List<Artikel>());

            var target = new ArtikelController(loggerMock.Object, serviceMock.Object);
            
            // Act

            var result = target.Get(categorieNaam);

            // Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            var view = result as JsonResult;

            Assert.IsInstanceOfType(view.Value, typeof(IEnumerable<ApiArtikelenModel>));
            var model = view.Value as IEnumerable<ApiArtikelenModel>;

            Assert.AreEqual(1, model.Count());

            var artikel = (model.First());
            Assert.AreEqual(5, artikel.Artikelnummer);
            Assert.AreEqual("Klein fiets slot", artikel.Beschrijving);
            Assert.AreEqual("images/bike_lock_small.gif", artikel.AfbeeldingUrl);
            Assert.AreEqual("Slot", artikel.Naam);
            Assert.AreEqual(28.32M, artikel.Prijs);
            Assert.AreEqual(8, artikel.Voorraad);
        }
    }
}
