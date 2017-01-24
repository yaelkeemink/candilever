using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CAN.Bestellingbeheer.Infrastructure.DAL;
using CAN.Bestellingbeheer.Domain.Entities;
using CAN.Bestellingbeheer.Infrastructure.Repositories;
using System;

namespace CAN.Bestellingbeheer.Infrastructure.Test
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
            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                //Arrange
                var bestelling = new Bestelling()
                {
                    VolledigeNaam = "Henk de Vries",
                    Klantnummer = 5,
                    Huisnummer = "344",
                    Adres = "Kalvestraat",
                    Postcode = "1234 AB",
                    Land = "Nederland",
                    BestelDatum = DateTime.Now,
                    Artikelen = new List<Artikel>
                    {
                        new Artikel
                        {
                            Artikelnummer = 5555,
                            Prijs = 2.50M,
                            Naam = "Mijn artikel",
                            Aantal = 5,
                            Leverancier = "De fietsleverancier",
                            LeverancierCode = "DFL",
                        }
                    }
                };

                //Act
                repo.Insert(bestelling);
            }

            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                //Assert
                Assert.AreEqual(1, repo.Count());
            }
        }

        [TestMethod]
        public void TestFind()
        {
            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                //Arrange
                var bestelling = new Bestelling()
                {
                    VolledigeNaam = "Henk de Vries",
                    Klantnummer = 5,
                    Huisnummer = "344",
                    Adres = "Kalvestraat",
                    Postcode = "1234 AB",
                    Land = "Nederland",
                    BestelDatum = DateTime.Now,
                    Artikelen = new List<Artikel>
                    {
                        new Artikel
                        {
                            Artikelnummer = 5555,
                            Prijs = 2.50M,
                            Naam = "Mijn artikel",
                            Aantal = 5,
                            Leverancier = "De fietsleverancier",
                            LeverancierCode = "DFL",
                        }
                    }
                };
                repo.Insert(bestelling);
            }

            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                //Act
                Bestelling result = repo.Find(1);

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Bestellingnummer);
                Assert.AreEqual("Henk de Vries", result.VolledigeNaam);
                Assert.AreEqual(5, result.Klantnummer);
                Assert.AreEqual("344", result.Huisnummer);
                Assert.AreEqual("Kalvestraat", result.Adres);
                Assert.AreEqual("1234 AB", result.Postcode);
                Assert.AreEqual("Nederland", result.Land);

                Artikel firstArtikel = result.Artikelen.First();
                Assert.IsNotNull(firstArtikel);
                Assert.AreEqual(5555, firstArtikel.Artikelnummer);
                Assert.AreEqual(2.50M, firstArtikel.Prijs);
                Assert.AreEqual("Mijn artikel", firstArtikel.Naam);
                Assert.AreEqual(5, firstArtikel.Aantal);
                Assert.AreEqual("De fietsleverancier", firstArtikel.Leverancier);
                Assert.AreEqual("DFL", firstArtikel.LeverancierCode);
            }
        }

        [TestMethod]
        public void TestDelete()
        {
            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                //Arrange
                var bestelling = new Bestelling()
                {
                    Artikelen = new List<Artikel>
                    {
                        new Artikel
                        {
                            Prijs = 2.50M
                        }
                    }
                };
                repo.Insert(bestelling);
            }
            
            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                //Act
                repo.Delete(1);
                //Assert
                Assert.AreEqual(0, repo.Count());
            }
            
        }

        [TestMethod]
        public void TestFindAll()
        {
            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                //Arrange
                var bestelling = new Bestelling()
                {
                    Artikelen = new List<Artikel>
                    {
                        new Artikel
                        {
                            Prijs = 2.50M
                        }
                    }
                };
                repo.Insert(bestelling);

                bestelling = new Bestelling()
                {
                    Artikelen = new List<Artikel>
                    {
                        new Artikel
                        {
                            Prijs = 3.50M
                        }
                    }
                };
                repo.Insert(bestelling);
            }
            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                //Act
                IEnumerable<Bestelling> result = repo.FindAll();

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Count());
                Assert.AreEqual(2, repo.Count());
            }
        }

        [TestMethod]
        public void TestUpdate()
        {
            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                //Arrange
                var bestelling = new Bestelling()
                {
                    VolledigeNaam = "Jan Willem de Koning",
                    Klantnummer = 6,
                    Huisnummer = "112",
                    Adres = "Herenweg",
                    Postcode = "1111 AA",
                    Land = "Belgie",
                    BestelDatum = DateTime.Now,
                    Artikelen = new List<Artikel>
                    {
                        new Artikel
                        {
                            Artikelnummer = 66,
                            Prijs = 10,
                            Naam = "Blauwe fiets",
                            Aantal = 1,
                            Leverancier = "Giant",
                            LeverancierCode = "GT",
                        }
                    }
                };
                repo.Insert(bestelling);
            }
            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                //Act
                Bestelling response = repo.Find(1);

                response.VolledigeNaam = "Henk de Vries";
                response.Klantnummer = 5;
                response.Huisnummer = "344";
                response.Adres = "Kalvestraat";
                response.Postcode = "1234 AB";
                response.Land = "Nederland";

                var artikel = response.Artikelen.First();
                artikel.Artikelnummer = 5555;
                artikel.Prijs = 2.50M;
                artikel.Naam = "Mijn artikel";
                artikel.Aantal = 5;
                artikel.Leverancier = "De fietsleverancier";
                artikel.LeverancierCode = "DFL";
                
                repo.Update(response);

                Bestelling result = repo.Find(1);

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Bestellingnummer);
                Assert.AreEqual("Henk de Vries", result.VolledigeNaam);
                Assert.AreEqual(5, result.Klantnummer);
                Assert.AreEqual("344", result.Huisnummer);
                Assert.AreEqual("Kalvestraat", result.Adres);
                Assert.AreEqual("1234 AB", result.Postcode);
                Assert.AreEqual("Nederland", result.Land);

                Artikel firstArtikel = result.Artikelen.First();
                Assert.IsNotNull(firstArtikel);
                Assert.AreEqual(5555, firstArtikel.Artikelnummer);
                Assert.AreEqual(2.50M, firstArtikel.Prijs);
                Assert.AreEqual("Mijn artikel", firstArtikel.Naam);
                Assert.AreEqual(5, firstArtikel.Aantal);
                Assert.AreEqual("De fietsleverancier", firstArtikel.Leverancier);
                Assert.AreEqual("DFL", firstArtikel.LeverancierCode);
            }
        }
    }
}
