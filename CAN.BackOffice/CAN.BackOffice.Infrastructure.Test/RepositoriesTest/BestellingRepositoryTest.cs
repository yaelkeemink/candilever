using CAN.BackOffice.Domain.Entities;
using CAN.BackOffice.Infrastructure.Test.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAN.BackOffice.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using CAN.BackOffice.Infrastructure.DAL.Repositories;

namespace CAN.BackOffice.Infrastructure.Test.RepositoriesTest
{
    [TestClass]
    public class BestellingRepositoryTest
    {
        private DbContextOptions<DatabaseContext> _dbOptions;

        [TestInitialize]
        public void Init()
        {
            _dbOptions = TestDatabaseProvider.CreateInMemoryDatabaseOptions();

        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddBestelling()
        {

            var bestelling = new Bestelling()
            {
                BestelDatum = new DateTime(2005, 10, 10),
                Klantnummer = 4,
                Bestellingsnummer = 3,
                BestellingStatusCode = "Besteld",
                BestellingStatusNumber = 1,
                Artikelen = new List<Artikel>()
                {
                    new Artikel() { Aantal = 1, Artikelnaam ="Wiel", Artikelnummer = 9, Leverancier = "Harm", LeverancierCode = "Takker", Prijs = 90.888M }
                }
            };

            using (var context = new DatabaseContext(_dbOptions))
            using (var repo = new BestellingRepository(context))
            {
                repo.Insert(bestelling);
                
            }

            using (var context = new DatabaseContext(_dbOptions))
            using (var repo = new BestellingRepository(context))
            {
                Assert.AreEqual(1, repo.Count());

                var result = repo.FindAll().First();
                Assert.AreEqual(bestelling.BestelDatum, result.BestelDatum);
                Assert.AreEqual(bestelling.Klantnummer, result.Klantnummer);
                Assert.AreEqual(bestelling.Bestellingsnummer, result.Bestellingsnummer);
                Assert.AreEqual(bestelling.BestellingStatusCode, result.BestellingStatusCode);
                Assert.AreEqual(bestelling.BestellingStatusNumber, result.BestellingStatusNumber);

                Assert.AreEqual(1, result.Artikelen.Count);

                Assert.AreEqual(bestelling.Artikelen.First().Aantal, result.Artikelen.First().Aantal);
                Assert.AreEqual(bestelling.Artikelen.First().Artikelnaam, result.Artikelen.First().Artikelnaam);
                Assert.AreEqual(bestelling.Artikelen.First().Artikelnummer, result.Artikelen.First().Artikelnummer);
                Assert.AreEqual(bestelling.Artikelen.First().Leverancier, result.Artikelen.First().Leverancier);
                Assert.AreEqual(bestelling.Artikelen.First().LeverancierCode, result.Artikelen.First().LeverancierCode);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddBestellingMeerdereAtrikellen()
        {

            var bestelling = new Bestelling()
            {
                BestelDatum = new DateTime(2005, 10, 10),
                Klantnummer = 4,
                Bestellingsnummer = 3,
                BestellingStatusCode = "Besteld",
                BestellingStatusNumber = 1,
                Artikelen = new List<Artikel>()
                {
                    new Artikel() { Aantal = 1, Artikelnaam ="Wiel", Artikelnummer = 9, Leverancier = "Harm", LeverancierCode = "Takker", Prijs = 90.888M },
                    new Artikel() { Aantal = 2, Artikelnaam ="Wiel", Artikelnummer = 10, Leverancier = "Harm", LeverancierCode = "Takker", Prijs = 90.888M },
                    new Artikel() { Aantal = 3, Artikelnaam ="Wiel", Artikelnummer = 11, Leverancier = "Harm", LeverancierCode = "Takker", Prijs = 90.888M }
                }
            };

            using (var context = new DatabaseContext(_dbOptions))
            using (var repo = new BestellingRepository(context))
            {
                repo.Insert(bestelling);

            }

            using (var context = new DatabaseContext(_dbOptions))
            using (var repo = new BestellingRepository(context))
            {
                Assert.AreEqual(1, repo.Count());

                var result = repo.FindAll().First();
                Assert.AreEqual(bestelling.BestelDatum, result.BestelDatum);
                Assert.AreEqual(bestelling.Klantnummer, result.Klantnummer);
                Assert.AreEqual(bestelling.Bestellingsnummer, result.Bestellingsnummer);
                Assert.AreEqual(bestelling.BestellingStatusCode, result.BestellingStatusCode);
                Assert.AreEqual(bestelling.BestellingStatusNumber, result.BestellingStatusNumber);

                Assert.AreEqual(3, result.Artikelen.Count);

            }
        }


        [TestMethod]
        public void TestVolgendeBestelling()
        {
            // arrage
            var bestelling1 = new Bestelling()
            {
                BestelDatum = new DateTime(2005, 10, 10),
                Klantnummer = 4,
                Bestellingsnummer = 5,
                BestellingStatusCode = "Goedgekeurd",
                BestellingStatusNumber = 0,
                Artikelen = new List<Artikel>()
                {
                    new Artikel() { Aantal = 1, Artikelnaam ="Wiel", Artikelnummer = 9, Leverancier = "Harm", LeverancierCode = "Takker", Prijs = 90.888M }
                }
            };

            var bestelling2 = new Bestelling()
            {
                BestelDatum = new DateTime(2006, 10, 10),
                Klantnummer = 4,
                Bestellingsnummer = 9,
                BestellingStatusCode = "Goedgekeurd",
                BestellingStatusNumber = 0,
                Artikelen = new List<Artikel>()
                {
                    new Artikel() { Aantal = 1, Artikelnaam ="Wiel", Artikelnummer = 9, Leverancier = "Harm", LeverancierCode = "Takker", Prijs = 90.888M }
                }
            };
            var bestelling3 = new Bestelling()
            {
                BestelDatum = new DateTime(2004, 10, 10),
                Klantnummer = 4,
                Bestellingsnummer = 11,
                BestellingStatusCode = "Goedgekeurd",
                BestellingStatusNumber = 0,
                Artikelen = new List<Artikel>()
                {
                    new Artikel() { Aantal = 1, Artikelnaam ="Wiel", Artikelnummer = 9, Leverancier = "Harm", LeverancierCode = "Takker", Prijs = 90.888M }
                }
            };

            using (var context = new DatabaseContext(_dbOptions))
            using (var repo = new BestellingRepository(context))
            {
                repo.Insert(bestelling1);
                repo.Insert(bestelling2);
                repo.Insert(bestelling3);

            }



            using (var context = new DatabaseContext(_dbOptions))
            using (var repo = new BestellingRepository(context))
            {
                Assert.AreEqual(3, repo.Count());
                // act
                var next = repo.FindBy(a => a.BestellingStatusCode == "Goedgekeurd")
                    .FirstOrDefault();
                // assert
                Assert.AreEqual(11, next.Bestellingsnummer);

            }

        }



        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestVolgendeBestellingMetUpdate()
        {
            // arrage
            var bestelling1 = new Bestelling()
            {
                BestelDatum = new DateTime(2001, 10, 10),
                Klantnummer = 4,
                Bestellingsnummer = 5,
                BestellingStatusCode = "Goedgekeurd",
                BestellingStatusNumber = 0,
                Artikelen = new List<Artikel>()
                {
                    new Artikel() { Aantal = 1, Artikelnaam ="Wiel", Artikelnummer = 9, Leverancier = "Harm", LeverancierCode = "Takker", Prijs = 90.888M }
                }
            };

            var bestelling2 = new Bestelling()
            {
                BestelDatum = new DateTime(2006, 10, 10),
                Klantnummer = 4,
                Bestellingsnummer = 9,
                BestellingStatusCode = "Goedgekeurd",
                BestellingStatusNumber = 0,
                Artikelen = new List<Artikel>()
                {
                    new Artikel() { Aantal = 1, Artikelnaam ="Wiel", Artikelnummer = 9, Leverancier = "Harm", LeverancierCode = "Takker", Prijs = 90.888M }
                }
            };
            var bestelling3 = new Bestelling()
            {
                BestelDatum = new DateTime(2004, 10, 10),
                Klantnummer = 4,
                Bestellingsnummer = 11,
                BestellingStatusCode = "Goedgekeurd",
                BestellingStatusNumber = 0,
                Artikelen = new List<Artikel>()
                {
                    new Artikel() { Aantal = 1, Artikelnaam ="Wiel", Artikelnummer = 9, Leverancier = "Harm", LeverancierCode = "Takker", Prijs = 90.888M }
                }
            };

            using (var context = new DatabaseContext(_dbOptions))
            using (var repo = new BestellingRepository(context))
            {
                repo.Insert(bestelling1);
                repo.Insert(bestelling2);
                repo.Insert(bestelling3);

            }



            using (var context = new DatabaseContext(_dbOptions))
            using (var repo = new BestellingRepository(context))
            {
                Assert.AreEqual(3, repo.Count());
                // act
                var next = repo.FindBy(a => a.BestellingStatusCode == "Goedgekeurd")
                    .FirstOrDefault();
                // assert
                Assert.AreEqual(5, next.Bestellingsnummer);
            }
            UpdateBestellingStatus(5);
            using (var context = new DatabaseContext(_dbOptions))
            using (var repo = new BestellingRepository(context))
            {
                var next = repo.FindBy(a => a.BestellingStatusCode == "Goedgekeurd")
                    .FirstOrDefault();
                // assert
                Assert.AreEqual(11, next.Bestellingsnummer);

            }

        }

        /// <summary>
        /// Fake update status event
        /// </summary>
        /// <param name="nr"></param>
        private void UpdateBestellingStatus(int nr)
        {
            using (var context = new DatabaseContext(_dbOptions))
            using (var repo = new BestellingRepository(context))
            {
                var bestelling = repo.Find(nr);
                bestelling.BestellingStatusCode = "Opgehaald";
                bestelling.BestellingStatusNumber = 100;
                repo.Update(bestelling);

            }
        }
    }
}
