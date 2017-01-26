using CAN.Webwinkel.Controllers;
using CAN.Webwinkel.Domain.Entities;
using CAN.Webwinkel.Domain.Interfaces;
using CAN.Webwinkel.Infrastructure.DAL;
using CAN.Webwinkel.Infrastructure.DAL.Repositories;
using CAN.Webwinkel.Infrastructure.Services;
using CAN.Webwinkel.Infrastructure.Test.RepositoriesTest;
using CAN.Webwinkel.ViewModels.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Test
{
    [TestClass]
    public class HomeControllerTests
    {

        [TestMethod]
        public void IndexRedirectTest()
        {
            //Arrange
            var homeControlLoggerMock = new Mock<ILogger<HomeController>>(MockBehavior.Loose);
            var artikelServiceMock = new Mock<IArtikelService>(MockBehavior.Strict);
            var winkelmandServiceMock = new Mock<IWinkelwagenService>(MockBehavior.Strict);

            HomeController homeControl = new HomeController(homeControlLoggerMock.Object, artikelServiceMock.Object, winkelmandServiceMock.Object);

            //Act
            var result = homeControl.Index() ;

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            var redirectToResult = result as RedirectToActionResult;

            Assert.AreEqual("id", redirectToResult.RouteValues.Keys.ToArray()[0]);
            Assert.AreEqual(1, (int)redirectToResult.RouteValues.Values.ToArray()[0]);
        }

        [TestMethod]
        public void IndexPageTest()
        {
            //Arrange
            DemoEntities demo = new DemoEntities();
            List<Artikel> artikelen = demo.CreateArtikelenList();

            var homeControlLoggerMock = new Mock<ILogger<HomeController>>(MockBehavior.Loose);

            var artikelServiceMock = new Mock<IArtikelService>(MockBehavior.Strict);
            artikelServiceMock.Setup(art => art.AlleArtikelenPerPagina(It.IsAny<int>(), It.IsAny<int>())).Returns(artikelen);
            artikelServiceMock.Setup(p => p.AantalPaginas(It.IsAny<int>())).Returns(4);

            var winkelmandServiceMock = new Mock<IWinkelwagenService>(MockBehavior.Strict);

            HomeController homeControl = new HomeController(homeControlLoggerMock.Object, artikelServiceMock.Object, winkelmandServiceMock.Object);

            //Act
            var result = homeControl.Index(2);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var viewResult = result as ViewResult;

            var ArtikelOverzichtView = viewResult.Model as ArtikelOverzichtViewModel;
            Assert.IsNotNull(viewResult.Model);

            Assert.AreEqual(3, ArtikelOverzichtView.Artikelen.Count());
            Assert.AreEqual(4, ArtikelOverzichtView.AantalPaginas);
            Assert.AreEqual(2, ArtikelOverzichtView.HuidigePaginanummer);
        }


        [TestMethod]
        public void ToonWinkelmandjeTest()
        {
            //Arrange
            DemoEntities demo = new DemoEntities();
            var demoDTOWinkelMand = demo.CreateArtikelenDTOList();
            var damesFietsPrijsBTW = Math.Round(demo.DamesFietsDTO.Prijs * 1.21M, 2);

            var homeControlLoggerMock = new Mock<ILogger<HomeController>>(MockBehavior.Loose);

            var artikelServiceMock = new Mock<IArtikelService>(MockBehavior.Strict);

            var winkelmandServiceMock = new Mock<IWinkelwagenService>(MockBehavior.Strict);
            winkelmandServiceMock.Setup(w => w.FindWinkelmandje(It.IsAny<string>())).Returns(demo.Winkelmand);

            HomeController homeControl = new HomeController(homeControlLoggerMock.Object, artikelServiceMock.Object, winkelmandServiceMock.Object);

            //Act
            var result = homeControl.ToonWinkelmandje("123456");

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var viewResult = result as ViewResult;

            Assert.IsInstanceOfType(viewResult.Model, typeof(WinkelmandjeViewModel));

            var winkelmandjeView = viewResult.Model as WinkelmandjeViewModel;

            Assert.IsNotNull(viewResult.Model);
            var winkelmandjeViewArtikelen = winkelmandjeView.Winkelmandje.Artikelen;

            Assert.AreEqual(1, winkelmandjeView.Winkelmandje.Id);
            Assert.AreEqual("123456", winkelmandjeView.Winkelmandje.WinkelmandjeNummer);

            Assert.AreEqual(3, winkelmandjeViewArtikelen.Count());
            Assert.IsFalse(winkelmandjeViewArtikelen.Contains(demo.HerenFietsDTO));
            Assert.AreNotEqual(259.67, winkelmandjeViewArtikelen[0].Prijs);
            Assert.AreEqual(144.4M, winkelmandjeViewArtikelen[0].Prijs);
        }
    }
}
