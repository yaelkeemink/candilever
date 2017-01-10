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
            _options = TestDatabaseProvider.CreateMsSQLDatabaseOptions();
        }

        [TestMethod]
        public void SaveArtikel()
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

                Assert.AreEqual(fiets.ArtikelCategory[0].CategoryId, herenFiets.ArtikelCategory[0].CategoryId);
            }
            

        }







    }
}
