using CAN.Klantbeheer.Domain.Entities;
using CAN.Klantbeheer.Domain.Interfaces;
using CAN.Klantbeheer.Domain.Services;
using InfoSupport.WSA.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CAN.Klantbeheer.Domain.Test
{
    [TestClass]
    public class serviceTest
    {
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
                repoMock.Setup(a => a.Insert(klant)).Returns(1);

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
                repoMock.Setup(a => a.Insert(klant)).Returns(1);

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
                repoMock.Setup(a => a.Insert(klant)).Returns(1);

                //Act
                var result = service.CreateKlant(klant);

                //Assert
                Assert.AreEqual(0, result);
            }
        }
    }
}
