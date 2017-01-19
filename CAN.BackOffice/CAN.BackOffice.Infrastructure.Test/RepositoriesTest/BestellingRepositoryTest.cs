using CAN.BackOffice.Infrastructure.Test.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAN.BackOffice.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using CAN.BackOffice.Domain.Entities;
using CAN.BackOffice.Infrastructure.DAL.Repositories;

namespace CAN.BackOffice.Infrastructure.Test.RepositoriesTest
{
    [TestClass]
    public class BestellingRepositoryTest
    {
        private DbContextOptions<DatabaseContext> _dbOtions;

        [TestInitialize]
        public void Init()
        {
            _dbOtions = TestDatabaseProvider.CreateInMemoryDatabaseOptions();
        }

        [TestMethod]
        public void AddKlantZonderTussenVoegsel()
        {

            //arrage
            var klant = new Klant()
            {
                Achternaam = "Gerritsen",
                Voornaam = "Henk",
                Adres = "Van Galenstraat 1",
                Email = "kantiliver@gmail.com",
                Huisnummer = "1",
                Land = "NL",
                Postcode = "2135AC",
                Klantnummer = 4,
                Telefoonnummer = "-1 12312312"
            };

            using (var context = new DatabaseContext(_dbOtions))
            using (var repo = new KlantRepository(context))
            {
                Assert.AreEqual(0, repo.Count());

                //act
                repo.Insert(klant);
                Assert.AreEqual(1, repo.Count());
            }
            using (var context = new DatabaseContext(_dbOtions))
            using (var repo = new KlantRepository(context))
            {
                //assert
                var klantFromRepo = repo.FindAll().First();
                Assert.IsNotNull(klantFromRepo);
                Assert.AreEqual(klant.Achternaam, klantFromRepo.Achternaam);
                Assert.AreEqual(klant.Voornaam, klantFromRepo.Voornaam);
                Assert.AreEqual(klant.Adres, klantFromRepo.Adres);
                Assert.AreEqual(klant.Email, klantFromRepo.Email);
                Assert.AreEqual(klant.Huisnummer, klantFromRepo.Huisnummer);
                Assert.AreEqual(klant.Land, klantFromRepo.Land);
                Assert.AreEqual(klant.Postcode, klantFromRepo.Postcode);
                Assert.AreEqual(klant.Klantnummer, klantFromRepo.Klantnummer);
                Assert.AreEqual(klant.Telefoonnummer, klantFromRepo.Telefoonnummer);
                Assert.IsNull(klant.Tussenvoegsel);

            }

        }


        [TestMethod]
        public void AddKlantMetTussenVoegsel()
        {

            //arrage
            var klant = new Klant()
            {
                Achternaam = "Gerritsen",
                Voornaam = "Henk",
                Adres = "Van Galenstraat 1",
                Email = "kantiliver@gmail.com",
                Huisnummer = "1",
                Land = "NL",
                Postcode = "2135AC",
                Klantnummer = 4,
                Telefoonnummer = "-1 12312312",
                Tussenvoegsel = "van"
            };

            using (var context = new DatabaseContext(_dbOtions))
            using (var repo = new KlantRepository(context))
            {
                Assert.AreEqual(0, repo.Count());

                //act
                repo.Insert(klant);
                Assert.AreEqual(1, repo.Count());
            }
            using (var context = new DatabaseContext(_dbOtions))
            using (var repo = new KlantRepository(context))
            {

                //assert
                var klantFromRepo = repo.FindAll().First();
                Assert.IsNotNull(klantFromRepo);
                Assert.AreEqual(klant.Achternaam, klantFromRepo.Achternaam);
                Assert.AreEqual(klant.Voornaam, klantFromRepo.Voornaam);
                Assert.AreEqual(klant.Adres, klantFromRepo.Adres);
                Assert.AreEqual(klant.Email, klantFromRepo.Email);
                Assert.AreEqual(klant.Huisnummer, klantFromRepo.Huisnummer);
                Assert.AreEqual(klant.Land, klantFromRepo.Land);
                Assert.AreEqual(klant.Postcode, klantFromRepo.Postcode);
                Assert.AreEqual(klant.Klantnummer, klantFromRepo.Klantnummer);
                Assert.AreEqual(klant.Telefoonnummer, klantFromRepo.Telefoonnummer);
                Assert.AreEqual(klant.Tussenvoegsel, klantFromRepo.Tussenvoegsel);
            }

        }


        [TestMethod]
        public void UpdateKlant()
        {

            //arrage
            var klant = new Klant()
            {
                Achternaam = "Gerritsen",
                Voornaam = "Henk",
                Adres = "Van Galenstraat 1",
                Email = "kantiliver@gmail.com",
                Huisnummer = "1",
                Land = "NL",
                Postcode = "2135AC",
                Klantnummer = 4,
                Telefoonnummer = "-1 12312312",
                Tussenvoegsel = "van"
            };

            using (var context = new DatabaseContext(_dbOtions))
            using (var repo = new KlantRepository(context))
            {
                Assert.AreEqual(0, repo.Count());

                //act
                repo.Insert(klant);
                Assert.AreEqual(1, repo.Count());
            }
            using (var context = new DatabaseContext(_dbOtions))
            using (var repo = new KlantRepository(context))
            {
                klant.Voornaam = "Rob";
                repo.Update(klant);
            }

            using (var context = new DatabaseContext(_dbOtions))
            using (var repo = new KlantRepository(context))
            {
                //assert
                var klantFromRepo = repo.FindAll().First();
                Assert.IsNotNull(klantFromRepo);
                Assert.AreEqual(klant.Achternaam, klantFromRepo.Achternaam);
                Assert.AreEqual(klant.Voornaam, klantFromRepo.Voornaam);
                Assert.AreEqual(klant.Adres, klantFromRepo.Adres);
                Assert.AreEqual(klant.Email, klantFromRepo.Email);
                Assert.AreEqual(klant.Huisnummer, klantFromRepo.Huisnummer);
                Assert.AreEqual(klant.Land, klantFromRepo.Land);
                Assert.AreEqual(klant.Postcode, klantFromRepo.Postcode);
                Assert.AreEqual(klant.Tussenvoegsel, klantFromRepo.Tussenvoegsel);
            }

        }

    }
}
