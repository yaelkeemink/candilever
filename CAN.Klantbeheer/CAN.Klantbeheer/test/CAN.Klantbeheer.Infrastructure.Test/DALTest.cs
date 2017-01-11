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
using CAN.Klantbeheer.Domain.Enums;

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
                    Telefoonnummer = "0640480381",                   
                    Postcode = "2361VJ", 
                    Adres = "van Leydenstraat",
                    Huisnummer = "14",
                    Email = "yaelkeemink@gmail.com",
                    Land = Land.Nederland,
                });
            }
            using (var repo = new KlantRepository(new DatabaseContext(_options)))
            {
                Assert.AreEqual(1, repo.Count());
            }
        }

        [TestMethod]
        public void TestAddMetPlusInTelefoonnummer()
        {

            using (var repo = new KlantRepository(new DatabaseContext(_options)))
            {
                repo.Insert(new Klant()
                {
                    Voornaam = "Yael",
                    Tussenvoegsels = "De",
                    Achternaam = "Keemink",
                    Telefoonnummer = "+31640480381",
                    Postcode = "2361VJ",
                    Adres = "van Leydenstraat",
                    Huisnummer = "14",
                    Email = "yaelkeemink@gmail.com",
                    Land = Land.Nederland,
                });
            }
            using (var repo = new KlantRepository(new DatabaseContext(_options)))
            {
                Assert.AreEqual(1, repo.Count());
            }
        }

        [TestMethod]
        public void TestAddMetToevoegingInHuisnummer()
        {

            using (var repo = new KlantRepository(new DatabaseContext(_options)))
            {
                repo.Insert(new Klant()
                {
                    Voornaam = "Yael",
                    Tussenvoegsels = "De",
                    Achternaam = "Keemink",
                    Telefoonnummer = "+31640480381",
                    Postcode = "2361VJ",
                    Adres = "van Leydenstraat",
                    Huisnummer = "14 A",
                    Email = "yaelkeemink@gmail.com",
                    Land = Land.Nederland,
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
                    Adres = "van Leydenstraat",
                    Huisnummer = "14",
                    Email = "yaelkeemink@gmail.com",
                    Telefoonnummer = "+31640480381",
                    Land = Land.Nederland,
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
                Assert.AreEqual("van Leydenstraat", result.Adres);
                Assert.AreEqual("14", result.Huisnummer);
                Assert.AreEqual("yaelkeemink@gmail.com", result.Email);
                Assert.AreEqual("+31640480381", result.Telefoonnummer);
                Assert.AreEqual(Land.Nederland, result.Land);
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
                    Adres = "van Leydenstraat",
                    Huisnummer = "14",
                    Email = "yaelkeemink@gmail.com",
                    Telefoonnummer = "+31640480381",
                    Land = Land.Nederland,
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
                    Adres = "van Leydenstraat",
                    Huisnummer = "14",
                    Email = "yaelkeemink@gmail.com",
                    Telefoonnummer = "+31640480381",
                    Land = Land.Nederland,
                };
                repo.Insert(klant);
                klant = new Klant()
                {
                    Voornaam = "Rob",
                    Achternaam = "Gerritsen",
                    Postcode = "1265DS",
                    Adres = "van Leydenstraat",
                    Huisnummer = "14",
                    Email = "yaelkeemink@gmail.com",
                    Telefoonnummer = "+31640480381",
                    Land = Land.Nederland,
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
                    Adres = "van Leydenstraat",
                    Huisnummer = "14",
                    Email = "yaelkeemink@gmail.com",
                    Telefoonnummer = "+31640480381",
                    Land = Land.Nederland,
                };
                repo.Insert(klant);
                klant = repo.Find(1);
                klant.Voornaam = "UpdatedName";
                repo.Update(klant);
            }

            using (var repo = new KlantRepository(new DatabaseContext(_options)))
            {
                var result = repo.Find(1);
                Assert.AreEqual(1, result.Klantnummer);
                Assert.AreEqual("UpdatedName", result.Voornaam);
                Assert.AreEqual("De", result.Tussenvoegsels);
                Assert.AreEqual("Keemink", result.Achternaam);
                Assert.AreEqual("2361VJ", result.Postcode);
                Assert.AreEqual("van Leydenstraat", result.Adres);
                Assert.AreEqual("14", result.Huisnummer);
                Assert.AreEqual("yaelkeemink@gmail.com", result.Email);
                Assert.AreEqual("+31640480381", result.Telefoonnummer);
                Assert.AreEqual(Land.Nederland, result.Land);
            }
        }
    }
}