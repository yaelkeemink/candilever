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
    public class KlantRepositoryTest
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
                Email = "kantiliver@gmail.com",
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
                var klantFromRepo = repo.Find(klant.Klantnummer);
                Assert.IsNotNull(klantFromRepo);
                Assert.AreEqual(klant.Email, klantFromRepo.Email);
                Assert.AreEqual(klant.Klantnummer, klantFromRepo.Klantnummer);
                Assert.AreEqual(klant.Telefoonnummer, klantFromRepo.Telefoonnummer);

            }

        }


        [TestMethod]
        public void AddKlantMetTussenVoegsel()
        {

            //arrage
            var klant = new Klant()
            {
                Email = "kantiliver@gmail.com",
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
                var klantFromRepo = repo.Find(klant.Klantnummer);
                Assert.IsNotNull(klantFromRepo);
                Assert.AreEqual(klant.Email, klantFromRepo.Email);
                Assert.AreEqual(klant.Klantnummer, klantFromRepo.Klantnummer);
                Assert.AreEqual(klant.Telefoonnummer, klantFromRepo.Telefoonnummer);
            }

        }
    }
}
