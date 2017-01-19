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
        [TestMethod]
        public void AddBestellingZonderBestaandeKlant()
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
                var next = repo.FindVolgendeBestelling();
                // assert
                Assert.AreEqual(11, next.Bestellingsnummer);

            }

        }



        [TestMethod]
        public void TestVolgendeBestelling2()
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
                var next = repo.FindVolgendeBestelling();
                // assert
                Assert.AreEqual(5, next.Bestellingsnummer);

            }

        }

    }
}
