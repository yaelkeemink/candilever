using CAN.BackOffice.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Serilog;
using CAN.Common.Events;
using CAN.BackOffice.Infrastructure.DAL.Repositories;
using CAN.BackOffice.Infrastructure.Test.Provider;
using System;
using CAN.BackOffice.Infrastructure.EventListener.Dispatchers;
using CAN.BackOffice.Domain.Entities;

namespace CAN.BackOffice.Infrastructure.Test.EventListener
{
    [TestClass]
    public class BestellingEventDispatcherTest
    {
        private DbContextOptions<DatabaseContext> _options;

        /// <summary>
        /// 
        /// </summary>
        [TestInitialize]
        public void Init()
        { 
            _options = TestDatabaseProvider.CreateInMemoryDatabaseOptions();
        }

        [TestMethod]
        public void BestellingAangemaaktEvent()
        {
            // No strict because we dont need it for this object
            var loggerMock = new Mock<ILogger>();
            using (var dispatcher = new BackOfficeEventDispatcher(null, _options, loggerMock.Object))
            {
                var currentDate = DateTime.Now;

                var bestellingEvent = new BestellingCreatedEvent("");
                bestellingEvent.Klantnummer = 1;
                bestellingEvent.Bestellingsnummer = 10;
                bestellingEvent.BestelDatum = currentDate;

                var artikelNummer = 15;
                var artikelNaam = "Fiets";
                var artikelPrijs = 2.50M;
                var artikelAantal = 5;
                var leverancier = "Batavus";
                var leverancierCode = "BTV";
                bestellingEvent.AddArtikel(artikelNummer, artikelNaam, artikelPrijs, artikelAantal, leverancier, leverancierCode); 

                dispatcher.BestellingAangemaakt(bestellingEvent);

                using (var context = new DatabaseContext(_options))
                using (var bestellingRepo = new BestellingRepository(context))
                {
                    Assert.AreEqual(1, bestellingRepo.Count());
                    
                    Bestelling bestelling = bestellingRepo.Find(bestellingEvent.Bestellingsnummer);

                    Assert.IsNotNull(bestelling);
                    Assert.AreEqual(currentDate, bestelling.BestelDatum);
                    Assert.IsNotNull(bestelling.Artikelen);
                    Assert.AreEqual(1, bestelling.Artikelen.Count);
                    var artikel = bestelling.Artikelen[0];
                    
                    Assert.AreEqual(bestellingEvent.Klantnummer, bestelling.Klantnummer);
                    Assert.AreEqual(artikelNummer, artikel.Artikelnummer);
                    Assert.AreEqual(artikelNaam, artikel.Artikelnaam);
                    Assert.AreEqual(artikelPrijs, artikel.Prijs);
                    Assert.AreEqual(artikelAantal, artikel.Aantal);
                    Assert.AreEqual(leverancier, artikel.Leverancier);
                    Assert.AreEqual(leverancierCode, artikel.LeverancierCode);
                }
            }
        }
    }
}
