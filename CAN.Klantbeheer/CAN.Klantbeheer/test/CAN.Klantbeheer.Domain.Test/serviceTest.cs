using CAN.Klantbeheer.Domain.Entities;
using CAN.Klantbeheer.Domain.Interfaces;
using CAN.Klantbeheer.Domain.Services;
using CAN.Klantbeheer.Infrastructure.DAL;
using CAN.Klantbeheer.Infrastructure.Repositories;
using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        public void TestAddZonderZonderTelefoonnummer()
        {
            //Arrange
            var publishMock = new Mock<IEventPublisher>();
            var repoMock = new Mock<IRepository<Klant, long>>();            
            using (var service = new KlantService(repoMock.Object, publishMock.Object))
            {
                var klant = new Klant
                {
                    Voornaam = "Yael",
                    Achternaam = "Keemink",
                    Tussenvoegsels = "de",
                    Postcode = "2361VJ",
                    Adres = "2361VJ",
                    Email = "yaelkeemink@gmail.com",
                    Huisnummer = "14",
                    Land = Enums.Land.Nederland,
                };                
                repoMock.Setup(a => a.Insert(klant));

                //Act
                var result = service.CreateKlant(klant);

                //Assert
                Assert.AreEqual(1, result);
            }
        }
        public void TestAddZonderZonderEmail()
        {
            //Arrange
            var publishMock = new Mock<IEventPublisher>();
            var repoMock = new Mock<IRepository<Klant, long>>();
            using (var service = new KlantService(repoMock.Object, publishMock.Object))
            {
                var klant = new Klant
                {
                    Voornaam = "Yael",
                    Achternaam = "Keemink",
                    Tussenvoegsels = "de",
                    Postcode = "2361VJ",
                    Adres = "2361VJ",
                    Telefoonnummer = "0640480381",
                    Huisnummer = "14",
                    Land = Enums.Land.Nederland,
                };
                repoMock.Setup(a => a.Insert(klant));

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
            using (var service = new KlantService(repoMock.Object, publishMock.Object))
            {
                var klant = new Klant
                {
                    Voornaam = "Yael",
                    Achternaam = "Keemink",
                    Tussenvoegsels = "de",
                    Postcode = "2361VJ",
                    Adres = "2361VJ",
                    Telefoonnummer = "0640480381",
                    Huisnummer = "14",
                    Land = Enums.Land.Nederland,
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
                using (var service = new KlantService(repo, publisherMock.Object))
                {
                    Klant klant = new Klant
                    {
                        Voornaam = "Yael",
                        Achternaam = "Keemink",
                        Tussenvoegsels = "de",
                        Postcode = "2361VJ",
                        Adres = "2361VJ",
                        Email = "yaelkeemink@gmail.com",
                        Huisnummer = "14",
                        Land = Enums.Land.Nederland,
                    };

                    Klant klant2 = new Klant
                    {
                        Voornaam = "Robert-Jan",
                        Achternaam = "Kooijman",
                        Tussenvoegsels = "",
                        Postcode = "2361VJ",
                        Adres = "2361VJ",
                        Email = "yaelkeemink@gmail.com",
                        Huisnummer = "14",
                        Land = Enums.Land.Nederland,
                    };
                    var returnval1 = service.CreateKlant(klant);
                    var returnval2 = service.CreateKlant(klant2);

                    Assert.AreNotEqual(returnval1, returnval2);
                }
            }
        }
    }
}
