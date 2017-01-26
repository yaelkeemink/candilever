using CAN.Klantbeheer.Domain.Entities;
using CAN.Klantbeheer.Domain.Interfaces;
using CAN.Klantbeheer.Infrastructure.DAL;
using CAN.Klantbeheer.Infrastructure.Repositories;
using CAN.Klantbeheer.Infrastructure.Services;
using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CAN.Klantbeheer.Domain.Test
{
    [TestClass]
    public class serviceTest
    {
        private static DbContextOptions<DatabaseContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.UseInMemoryDatabase()
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
        [TestMethod]
        public void TestAddZonderTelefoonnummer()
        {
            //Arrange
            var publishMock = new Mock<IEventPublisher>();
            var repoMock = new KlantRepository(new DatabaseContext(CreateNewContextOptions()));
            var loggerMock = new Mock<ILogger<KlantService>>();
            using (var service = new KlantService(repoMock, publishMock.Object, loggerMock.Object))
            {
                var klant = new Klant
                {
                    Voornaam = "Yael",
                    Achternaam = "Keemink",
                    Tussenvoegsels = "de",
                    Postcode = "2361VJ",
                    Straatnaam = "2361VJ",
                    Email = "yaelkeemink@gmail.com",
                    Huisnummer = "14",
                    Land = "Nederland",
                };

                //Act
                var result = service.CreateKlant(klant);

                //Assert
                Assert.AreEqual(1, result);
            }
        }
        public void TestAddZonderEmail()
        {
            //Arrange
            var publishMock = new Mock<IEventPublisher>();
            var repoMock = new KlantRepository(new DatabaseContext(CreateNewContextOptions()));
            var loggerMock = new Mock<ILogger<KlantService>>();
            using (var service = new KlantService(repoMock, publishMock.Object, loggerMock.Object))
            {
                var klant = new Klant
                {
                    Voornaam = "Yael",
                    Achternaam = "Keemink",
                    Tussenvoegsels = "de",
                    Postcode = "2361VJ",
                    Straatnaam = "2361VJ",
                    Telefoonnummer = "0640480381",
                    Huisnummer = "14",
                    Land = "Nederland",
                };

                //Act
                var result = service.CreateKlant(klant);

                //Assert
                Assert.AreEqual(1, result);
            }
        }
        public void TestAddZonderZonderEmailEnTelefoonnummer()
        {
            //Arrange
            var publishMock = new Mock<IEventPublisher>();
            var repoMock = new Mock<IRepository<Klant, long>>();
            var loggerMock = new Mock<ILogger<KlantService>>();
            using (var service = new KlantService(repoMock.Object, publishMock.Object, loggerMock.Object))
            {
                var klant = new Klant
                {
                    Voornaam = "Yael",
                    Achternaam = "Keemink",
                    Tussenvoegsels = "de",
                    Postcode = "2361VJ",
                    Straatnaam = "2361VJ",
                    Telefoonnummer = "0640480381",
                    Huisnummer = "14",
                    Land = "Nederland",
                };
                repoMock.Setup(a => a.Insert(klant));

                //Act
                var result = service.CreateKlant(klant);

                //Assert
                Assert.AreEqual(0, result);
            }
        }

        [TestMethod]
        public void BUGInsertMeerdereKlantenZelfdeReturn()
        {
            var options = CreateNewContextOptions();

            using (var context = new DatabaseContext(options))
            {
                var repo = new KlantRepository(context);
                var publisherMock = new Mock<IEventPublisher>();
                var loggerMock = new Mock<ILogger<KlantService>>();
                using (var service = new KlantService(repo, publisherMock.Object, loggerMock.Object))
                {
                    Klant klant_yael = new Klant
                    {
                        Voornaam = "Yael",
                        Achternaam = "Keemink",
                        Tussenvoegsels = "de",
                        Postcode = "2361VJ",
                        Straatnaam = "2361VJ",
                        Email = "yaelkeemink@gmail.com",
                        Huisnummer = "14",
                        Land = "Nederland",
                    };

                    Klant klant_rj = new Klant
                    {
                        Voornaam = "Robert-Jan",
                        Achternaam = "Kooijman",
                        Tussenvoegsels = "",
                        Postcode = "2361VJ",
                        Straatnaam = "2361VJ",
                        Email = "yaelkeemink@gmail.com",
                        Huisnummer = "14",
                        Land = "Nederland",
                    };
                    var returnval1 = service.CreateKlant(klant_yael);
                    var returnval2 = service.CreateKlant(klant_rj);

                    Assert.AreEqual(1, returnval1);
                    Assert.AreEqual(2, returnval2);
                    Assert.AreNotEqual(returnval1, returnval2);
                }
            }
        }
    }
}
