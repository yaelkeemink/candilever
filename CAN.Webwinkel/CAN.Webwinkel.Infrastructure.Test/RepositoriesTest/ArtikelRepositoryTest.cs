using CAN.Webwinkel.Infrastructure.DAL;
using CAN.Webwinkel.Infrastructure.DAL.Repositories;
using CAN.Webwinkel.Infrastructure.Test.Provider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

                Assert.IsNotNull(fiets.ArtikelCategorie[0].Categorie);
                Assert.IsNotNull(fiets.ArtikelCategorie[0].Categorie.Naam);
                Assert.AreEqual(herenFiets.ArtikelCategorie[0].Categorie.Naam, fiets.ArtikelCategorie[0].Categorie.Naam);

            }
        }


        [TestMethod]
        public void SaveArtikelCheckCategory()
        {
            var demo = new DemoEntities();

            using (var context = new WinkelDatabaseContext(_options))
            using (var repo = new ArtikelRepository(context))
            {
                repo.Insert(demo.HerenFiets);
                repo.Insert(demo.Fiets);
                repo.Insert(demo.DamesFiets);
            }

            using (var context = new WinkelDatabaseContext(_options))
            using (var repo = new ArtikelRepository(context))
            {
                Assert.AreEqual(3, repo.Count());

                var fiets = repo.Find(demo.Fiets.Artikelnummer);
                var herenFiets = repo.Find(demo.HerenFiets.Artikelnummer);

                Assert.AreEqual(fiets.ArtikelCategorie[0].CategoryId, herenFiets.ArtikelCategorie[0].CategoryId);
            }
        }

        [TestMethod]
        public void UpdateArtikelTest()
        {
            //Arrange
            var demo = new DemoEntities();

            var herenfiets = demo.HerenFiets;

            using (var context = new WinkelDatabaseContext(_options))
            using (var repo = new ArtikelRepository(context))
            {
                repo.Insert(herenfiets);
            }

            using (var context = new WinkelDatabaseContext(_options))
            {
                using (var repo = new ArtikelRepository(context))
                {
                    //Act
                    herenfiets.Prijs = 2000;
                    var success = repo.Update(herenfiets);

                    //Assert
                    var fiets = repo.FindBy(a => a.Artikelnummer == herenfiets.Artikelnummer).Single();
                    
                    Assert.AreNotEqual(fiets.Prijs, demo.HerenFiets);
                }
            }
        }

        [TestMethod]
        public void DeleteArtikelFromDatabaseTest()
        {
            //Arrange
            var demo = new DemoEntities();

            var herenfiets = demo.HerenFiets;

            using (var context = new WinkelDatabaseContext(_options))
            using (var repo = new ArtikelRepository(context))
            {
                repo.Insert(herenfiets);
            }

            using (var context = new WinkelDatabaseContext(_options))
            {
                using (var repo = new ArtikelRepository(context))
                {
                    //Act
                    var success = repo.Delete((int)herenfiets.Id);

                    //Assert
                    var fiets = repo.FindBy(a => a.Artikelnummer == herenfiets.Artikelnummer).SingleOrDefault();

                    Assert.AreEqual(null, fiets);
                }
            }
        }
    }
}
