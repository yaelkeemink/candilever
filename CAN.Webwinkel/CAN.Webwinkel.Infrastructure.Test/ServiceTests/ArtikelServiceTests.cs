using CAN.Webwinkel.Domain.Services;
using CAN.Webwinkel.Infrastructure.DAL;
using CAN.Webwinkel.Infrastructure.DAL.Repositories;
using CAN.Webwinkel.Infrastructure.Test.Provider;
using CAN.Webwinkel.Infrastructure.Test.RepositoriesTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Infrastructure.Test.ServiceTests
{
    [TestClass]
    public class ArtikelServiceTests
    {
        private DbContextOptions<WinkelDatabaseContext> _options;
        private ArtikelService service;
        /// <summary>
        /// 
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            _options = TestDatabaseProvider.CreateInMemoryDatabaseOptions();

            var context = new WinkelDatabaseContext(_options);
            var repo = new ArtikelRepository(context);
            ILogger<ArtikelService> logger = null;
            service = new ArtikelService(logger, repo);
        }

        [TestMethod]
        public void ArtikelenBijCategorieTest()
        {
            //Arrange
            var demo = new DemoEntities();
            var herenFiets = demo.HerenFiets;

            using (var context = new WinkelDatabaseContext(_options))
            using (var repo = new ArtikelRepository(context))
            {
                repo.Insert(demo.HerenFiets);
                repo.Insert(demo.DamesFiets);
                repo.Insert(demo.Fiets);
            }

            using (var context = new WinkelDatabaseContext(_options))
            using (var repo = new ArtikelRepository(context))
            {
                //Act
                var artikelen = service.ArtikelenBijCategorie("Heren fiets").ToArray();

                //Assert
                var herenfiets = artikelen[0];
                var fiets = artikelen[1];

                Assert.AreEqual(demo.HerenFiets.Id, herenfiets.Id);
                Assert.AreEqual(demo.Fiets.Id, fiets.Id);
            }
        }

        [TestMethod]
        public void AlleArtikelenTest()
        {
            //Arrange
            var demo = new DemoEntities();
            var herenFiets = demo.HerenFiets;

            using (var context = new WinkelDatabaseContext(_options))
            using (var repo = new ArtikelRepository(context))
            {
                repo.Insert(demo.HerenFiets);
                repo.Insert(demo.DamesFiets);
                repo.Insert(demo.Fiets);
            }

            using (var context = new WinkelDatabaseContext(_options))
            using (var repo = new ArtikelRepository(context))
            {
                //Act
                var alleArtikelen = service.AlleArtikelen();
               
                //Assert
                var artikelenLength = alleArtikelen.Count();

                Assert.AreEqual(3, artikelenLength);
            }
        }

        [TestMethod]
        public void AlleArtikelenPerPaginaTest()
        {
            //Arrange
            var demo = new DemoEntities();
            var customHerenFiets = demo.HerenFiets;
            customHerenFiets.Id = 4;

            var customDamesFiets = demo.DamesFiets;
            customDamesFiets.Id = 5;

            using (var context = new WinkelDatabaseContext(_options))
            using (var repo = new ArtikelRepository(context))
            {
                //Dubble Inserts Only for testing.
                repo.Insert(demo.HerenFiets);
                repo.Insert(demo.DamesFiets);
                repo.Insert(demo.Fiets);

                repo.Insert(customHerenFiets);
                repo.Insert(customDamesFiets);
            }

            using (var context = new WinkelDatabaseContext(_options))
            using (var repo = new ArtikelRepository(context))
            {
                //Act
                var paginaNummer = 2;
                var aantalArtikelen = 3;
                var alleArtikelenOpPagina = service.AlleArtikelenPerPagina(paginaNummer, aantalArtikelen).ToArray();

                //Assert
                var eersteArtikelOpPagina = alleArtikelenOpPagina[0];
                var tweedeArtikelOpPagina = alleArtikelenOpPagina[1];

                var artikelenLength = alleArtikelenOpPagina.Count();

                Assert.AreEqual(customHerenFiets.Id, eersteArtikelOpPagina.Id);
                Assert.AreEqual(customDamesFiets.Id, tweedeArtikelOpPagina.Id);
                Assert.AreEqual(2, artikelenLength); // Er zijn 5 inserts gedaan er er worden 3 artikelen overgeslagen.
            }
        }
    }
}
