using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CAN.Klantbeheer.Infrastructure.DAL;
using CAN.Klantbeheer.Infrastructure.Repositories;
using CAN.Klantbeheer.Domain.Entities;

namespace CAN.Klantbeheer.Infrastructure.Test.Test
{
    [TestClass]
    public class DALTest
    {
        private DbContextOptions _options;
        private static DbContextOptions<DatabaseContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [TestInitialize]
        public void Init()
        {
            _options = CreateNewContextOptions();
        }

        [TestMethod]
        public void TestAdd()
        {

            using (var repo = new KlantRepository(new DatabaseContext(_options)))
            {
                repo.Insert(new Klant()
                {
                    Voornaam = "Yael",
                    Tussenvoegsels = "De",
                    Achternaam = "Keemink",                    
                    Postcode = "2361VJ",                    
                });
            }
            using (var repo = new KlantRepository(new DatabaseContext(_options)))
            {
                Assert.AreEqual(1, repo.Count());
            }
        }

        [TestMethod]
        public void TestFind()
        {
            using (var repo = new KlantRepository(new DatabaseContext(_options)))
            {
                repo.Insert(new Klant()
                {
                    Voornaam = "Yael",
                    Tussenvoegsels = "De",
                    Achternaam = "Keemink",                    
                    Postcode = "2361VJ",
                });
            }

            using (var repo = new KlantRepository(new DatabaseContext(_options)))
            {
                var result = repo.Find(1);
                Assert.AreEqual(1, result.Klantnummer);
                Assert.AreEqual("De", result.Tussenvoegsels);
                Assert.AreEqual("Yael", result.Voornaam);                
                Assert.AreEqual("Keemink", result.Achternaam);
                Assert.AreEqual("2361VJ", result.Postcode);
            }
        }
        [TestMethod]
        public void TestDelete()
        {
            using (var repo = new KlantRepository(new DatabaseContext(_options)))
            {
                var klant = new Klant()
                {
                    Voornaam = "Yael",
                    Tussenvoegsels = "De",
                    Achternaam = "Keemink",
                    Postcode = "2361VJ",
                };
                repo.Insert(klant);
                repo.Delete(1);
            }

            using (var repo = new KlantRepository(new DatabaseContext(_options)))
            {
                Assert.AreEqual(0, repo.Count());
            }
        }
        [TestMethod]
        public void TestFindAll()
        {
            using (var repo = new KlantRepository(new DatabaseContext(_options)))
            {
                var klant = new Klant()
                {
                    Voornaam = "Yael",
                    Tussenvoegsels = "De",
                    Achternaam = "Keemink",
                    Postcode = "2361VJ",
                };
                repo.Insert(klant);
                klant = new Klant()
                {
                    Voornaam = "Rob",
                    Achternaam = "Gerritsen",
                    Postcode = "1265DS",
                };
                repo.Insert(klant);
            }

            using (var repo = new KlantRepository(new DatabaseContext(_options)))
            {
                Assert.AreEqual(2, repo.Count());
            }
        }
        [TestMethod]
        public void TestUpdate()
        {
            using (var repo = new KlantRepository(new DatabaseContext(_options)))
            {
                var klant = new Klant()
                {
                    Voornaam = "Yael",
                    Tussenvoegsels = "De",
                    Achternaam = "Keemink",
                    Postcode = "2361VJ",
                };
                repo.Insert(klant);
                klant = repo.Find(1);
                klant.Voornaam = "UpdatedName";
                repo.Update(klant);
            }

            using (var repo = new KlantRepository(new DatabaseContext(_options)))
            {
                var klant = repo.Find(1);
                Assert.AreEqual(1, klant.Klantnummer);
                Assert.AreEqual("UpdatedName", klant.Voornaam);
            }
        }
    }
}