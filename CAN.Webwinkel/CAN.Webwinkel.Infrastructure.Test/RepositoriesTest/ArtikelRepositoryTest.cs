using CAN.Webwinkel.Infrastructure.DAL;
using CAN.Webwinkel.Infrastructure.DAL.Repositories;
using CAN.Webwinkel.Infrastructure.Test.Provider;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Infrastructure.Test.RepositoriesTest
{
    [TestClass]
    public class ArtikelRepositoryTest
    {
        private DbContextOptions _options;

        /// <summary>
        /// 
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Use InMemory database for testing, records are not removed afterwards from Local Database
            //_options = TestDatabaseProvider.CreateInMemoryDatabaseOptions();
            _options = TestDatabaseProvider.CreateInMemoryDatabaseOptions();
        }


        [TestMethod]
        public void SaveArtikel()
        {

            var demo = new DemoEntities();
            var herenFiets = demo.HerenFiets;
            using (var context = new WinkelDatabaseContext(_options))
            using (var repo = new ArtikelRepository(context))
            {
                repo.Insert(herenFiets);

            }

            using (var context = new WinkelDatabaseContext(_options))
            using (var repo = new ArtikelRepository(context))
            {
                Assert.AreEqual(1, repo.Count());
                
                var fiets = repo.Find(demo.HerenFiets.Artikelnummer);
                Assert.AreEqual(herenFiets.Artikelnummer, fiets.Artikelnummer);
                Assert.AreEqual(herenFiets.AfbeeldingUrl, fiets.AfbeeldingUrl);
                Assert.AreEqual(herenFiets.Beschrijving, fiets.Beschrijving);
                Assert.AreEqual(herenFiets.LeverbaarTot, fiets.LeverbaarTot);
                Assert.AreEqual(herenFiets.Leverancier, fiets.Leverancier);
                Assert.AreEqual(herenFiets.LeverbaarVanaf, fiets.LeverbaarVanaf);
                Assert.AreEqual(herenFiets.LeverancierCode, fiets.LeverancierCode);
                Assert.AreEqual(herenFiets.Prijs, fiets.Prijs);
                Assert.AreEqual(herenFiets.Naam, fiets.Naam);
            }
        }
    }
}
