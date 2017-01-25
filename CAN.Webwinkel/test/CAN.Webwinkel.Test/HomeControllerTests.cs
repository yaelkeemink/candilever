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
        private HomeController homeControl;
        private DbContextOptions<WinkelDatabaseContext> _options;
        private WinkelDatabaseContext _context;
        private static ArtikelService aService;
        private static ArtikelRepository _repo;


        //public void Init()
        //{
        //    ILogger<HomeController> _logger = null;

        //    ILogger<ArtikelService> artikel_logger = null;

        //    _options = TestDatabaseProvider.CreateInMemoryDatabaseOptions();
        //    _context = new WinkelDatabaseContext(_options);

        //    ArtikelRepository aRepo = new ArtikelRepository(_context);
        //    aService = new ArtikelService(artikel_logger, aRepo);


        //    WinkelmandjeRepository cartRepo = new WinkelmandjeRepository(_context);

        //    WinkelmandjeService winkelmandjeservice = new WinkelmandjeService(cartRepo);
        //    homeControl = new HomeController(_logger, aService, winkelmandjeservice);
        //}

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
            var redirectToResult = result as RedirectToActionResult;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));

            Assert.IsNotNull(redirectToResult);

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
            var viewResult = result as ViewResult;
            var ArtikelOverzichtView = viewResult.Model as ArtikelOverzichtViewModel ;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));

            Assert.IsNotNull(viewResult);
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
            var viewResult = result as ViewResult;
            var winkelmandjeView = viewResult.Model as WinkelmandjeViewModel;
            var winkelmandjeViewArtikelen = winkelmandjeView.winkelmandje.Artikelen;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));

            Assert.IsNotNull(viewResult);
            Assert.IsNotNull(viewResult.Model);

            Assert.AreEqual(1, winkelmandjeView.winkelmandje.Id);
            Assert.AreEqual("123456", winkelmandjeView.winkelmandje.WinkelmandjeNummer);

            Assert.AreEqual(demoDTOWinkelMand.Count(), winkelmandjeViewArtikelen.Count());
            Assert.IsFalse(winkelmandjeViewArtikelen.Contains(demo.HerenFietsDTO));
            Assert.AreNotEqual(demo.HerenFietsDTO.Prijs, winkelmandjeViewArtikelen[0].Prijs);
            Assert.AreEqual(damesFietsPrijsBTW, winkelmandjeViewArtikelen[0].Prijs);
        }
    }
}
