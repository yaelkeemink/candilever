using CAN.BackOffice.Infrastructure.DAL;
using CAN.BackOffice.Infrastructure.Test.Provider;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Serilog;
using CAN.Common.Events;
using CAN.BackOffice.Infrastructure.EventListener.Dispatchers;
using CAN.BackOffice.Infrastructure.DAL.Repositories;
using CAN.BackOffice.Domain.Entities;

namespace CAN.BackOffice.Infrastructure.Test.EventListener
{
    [TestClass]
    public class KlantEventDispatcherTest
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
        public void KlantAangemaaktEvent()
        {
            // No strict because we dont need it for this object
            var loggerMock = new Mock<ILogger>();
            using (var dispatcher = new  BackOfficeEventDispatcher(null, _options, loggerMock.Object))
            {
                var klantEvent = new KlantCreatedEvent("");

                klantEvent.Klantnummer = 1;
                klantEvent.Voornaam = "Henk";
                klantEvent.Achternaam = "Jansen";
                klantEvent.Adres = "Kalvestraat";
                klantEvent.Postcode = "1234 RR";
                klantEvent.Telefoonnummer = "0699988771";
                klantEvent.Huisnummer = "33";
                klantEvent.Email = "henkjansen@kantilever.nl";
                klantEvent.Land = "Nederland";

                dispatcher.KlantAangemaakt(klantEvent);

                using (var context = new DatabaseContext(_options))
                using (var klantRepo = new KlantRepository(context))
                {
                    Assert.AreEqual(1, klantRepo.Count());

                    Klant klant = klantRepo.Find(klantEvent.Klantnummer);
                    Assert.IsNotNull(klant);

                    Assert.AreEqual(klantEvent.Voornaam, klant.Voornaam);
                    Assert.AreEqual(klantEvent.Achternaam, klant.Achternaam);
                    Assert.AreEqual(klantEvent.Adres, klant.Adres);
                    Assert.AreEqual(klantEvent.Postcode, klant.Postcode);
                    Assert.AreEqual(klantEvent.Telefoonnummer, klant.Telefoonnummer);
                    Assert.AreEqual(klantEvent.Huisnummer, klant.Huisnummer);
                    Assert.AreEqual(klantEvent.Email, klant.Email);
                    Assert.AreEqual(klantEvent.Land, klant.Land);
                }
            }
        }
    }
}
