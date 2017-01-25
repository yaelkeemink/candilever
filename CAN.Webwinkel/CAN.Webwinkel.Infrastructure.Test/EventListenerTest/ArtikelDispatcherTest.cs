using CAN.Webwinkel.Infrastructure.Test.Provider;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using CAN.Webwinkel.Infrastructure.DAL;
using CAN.Webwinkel.Infrastructure.EventListener.Dispatchers;
using CAN.Webwinkel.Infrastructure.DAL.Repositories;

namespace CAN.Webwinkel.Infrastructure.Test.EventListenerTest
{
    [TestClass]
    public class ArtikelDispatcherTest
    {

        private DbContextOptions<WinkelDatabaseContext> _options;

        /// <summary>
        /// 
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            _options = TestDatabaseProvider.CreateInMemoryDatabaseOptions();
        }

        [TestMethod]
        public void ArtikelToegevoegdAanCatalogus()
        {
            // No strict because we dont need it for this object
            var loggerMock = new Mock<ILogger>();
            using (var dispatcher = new ArtikelEventDispatcher(null, _options, loggerMock.Object))
            {
                var artikelEvent = new EventProvider().FietsCreatedEvent;


                dispatcher.ArtikelAanCatalogusToegevoegd(artikelEvent);

                using (var context = new WinkelDatabaseContext(_options))
                using (var artikelRepo = new ArtikelRepository(context))
                {

                    Assert.AreEqual(1, artikelRepo.Count());

                    var fiets = artikelRepo.Find(artikelEvent.Artikelnummer);
                    Assert.AreEqual(artikelEvent.Artikelnummer, fiets.Artikelnummer);
                    Assert.AreEqual(artikelEvent.AfbeeldingUrl, fiets.AfbeeldingUrl);
                    Assert.AreEqual(artikelEvent.Beschrijving, fiets.Beschrijving);
                    Assert.AreEqual(artikelEvent.LeverbaarTot, fiets.LeverbaarTot);
                    Assert.AreEqual(artikelEvent.Leverancier, fiets.Leverancier);
                    Assert.AreEqual(artikelEvent.LeverbaarVanaf, fiets.LeverbaarVanaf);
                    Assert.AreEqual(artikelEvent.LeverancierCode, fiets.LeverancierCode);
                    Assert.AreEqual(artikelEvent.Prijs, fiets.Prijs);
                    Assert.AreEqual(artikelEvent.Naam, fiets.Naam);
                    Assert.AreEqual(0, fiets.Voorraad);                   
                }
            }
        }


        [TestMethod]
        public void ArtikelVerwijdertUitCatalogus()
        {
            // No strict because we dont need it for this object
            var loggerMock = new Mock<ILogger>();
            using (var dispatcher = new ArtikelEventDispatcher(null, _options, loggerMock.Object))
            {
                var eventProvider = new EventProvider();
                dispatcher.ArtikelAanCatalogusToegevoegd(eventProvider.FietsCreatedEvent);
                dispatcher.ArtikelAanCatalogusToegevoegd(eventProvider.HerenFietsCreatedEvent);

                using (var context = new WinkelDatabaseContext(_options))
                using (var artikelRepo = new ArtikelRepository(context))
                {
                    Assert.AreEqual(2, artikelRepo.Count());
                    dispatcher.ArtikelUitCatalogusVerwijderd(eventProvider.VerwijderFietsEvent);

                    Assert.AreEqual(1, artikelRepo.Count());
                    Assert.ThrowsException<InvalidOperationException>(() => artikelRepo.Find(eventProvider.VerwijderFietsEvent.Artikelnummer));
                }
            }
        }


        [TestMethod]
        public void VerhoogVoorrraadTest()
        {
            // No strict because we dont need it for this object
            var loggerMock = new Mock<ILogger>();
            using (var dispatcher = new ArtikelEventDispatcher(null, _options, loggerMock.Object))
            {
                var eventProvider = new EventProvider();

                dispatcher.ArtikelAanCatalogusToegevoegd(eventProvider.FietsCreatedEvent);

                using (var context = new WinkelDatabaseContext(_options))
                using (var artikelRepo = new ArtikelRepository(context))
                {

                    Assert.AreEqual(1, artikelRepo.Count());
                    var fiets = artikelRepo.Find(eventProvider.FietsCreatedEvent.Artikelnummer);
                    Assert.AreEqual(0, fiets.Voorraad);

                }

                dispatcher.ArtikelInMagazijnGezet(eventProvider.VerhoogVoorraadEvent);

                using (var context = new WinkelDatabaseContext(_options))
                using (var artikelRepo = new ArtikelRepository(context))
                {

                    Assert.AreEqual(1, artikelRepo.Count());
                    var fiets = artikelRepo.Find(eventProvider.FietsCreatedEvent.Artikelnummer);
                    Assert.AreEqual(eventProvider.VerhoogVoorraadEvent.Voorraad, fiets.Voorraad);

                }
            }
        }





        [TestMethod]
        public void VerlaagVoorraad()
        {
            // No strict because we dont need it for this object
            var loggerMock = new Mock<ILogger>();

            using (var dispatcher = new ArtikelEventDispatcher(null, _options, loggerMock.Object))
            {
                var eventProvider = new EventProvider();

                dispatcher.ArtikelAanCatalogusToegevoegd(eventProvider.FietsCreatedEvent);
                dispatcher.ArtikelInMagazijnGezet(eventProvider.VerhoogVoorraadEvent);
                using (var context = new WinkelDatabaseContext(_options))
                using (var artikelRepo = new ArtikelRepository(context))
                {

                    Assert.AreEqual(1, artikelRepo.Count());
                    var fiets = artikelRepo.Find(eventProvider.FietsCreatedEvent.Artikelnummer);
                    Assert.AreEqual(eventProvider.VerhoogVoorraadEvent.Voorraad, fiets.Voorraad);

                }

                dispatcher.ArtikelUitMagazijnGehaald(eventProvider.VerlaagVoorraadEvent);


                using (var context = new WinkelDatabaseContext(_options))
                using (var artikelRepo = new ArtikelRepository(context))
                {
                    Assert.AreEqual(1, artikelRepo.Count());
                    var fiets = artikelRepo.Find(eventProvider.FietsCreatedEvent.Artikelnummer);
                    Assert.AreEqual(eventProvider.VerlaagVoorraadEvent.Voorraad, fiets.Voorraad);

                }
            }

        }

    }
}
