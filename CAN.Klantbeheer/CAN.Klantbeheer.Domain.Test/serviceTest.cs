using CAN.Klantbeheer.Domain.Entities;
using CAN.Klantbeheer.Domain.Services;
using CAN.Klantbeheer.Domain.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Klantbeheer.Domain.Test
{
    [TestClass]
    public class serviceTest
    {
        [TestMethod]
        public void TestAddZonderZonderTelefoonnummer()
        {
            //Arrange
            var publishMock = new PublishMock();
            var repoMock = new RepoMock();
            using (var service = new KlantService(repoMock, publishMock))
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
                    Land = "Nederland",
                };

                //Act
                var result = service.CreateKlant(klant);

                //Assert
                Assert.AreEqual(1, result);
            }
        }
        public void TestAddZonderZonderEmail()
        {
            //Arrange
            var publishMock = new PublishMock();
            var repoMock = new RepoMock();
            using (var service = new KlantService(repoMock, publishMock))
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
            var publishMock = new PublishMock();
            var repoMock = new RepoMock();
            using (var service = new KlantService(repoMock, publishMock))
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
                    Land = "Nederland",
                };

                //Act
                var result = service.CreateKlant(klant);

                //Assert
                Assert.AreEqual(0, result);
            }
        }
    }
}
