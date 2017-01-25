

using CAN.Webwinkel.Domain.Services;
using CAN.Webwinkel.Infrastructure.DAL;
using CAN.Webwinkel.Infrastructure.DAL.Repositories;
using CAN.Webwinkel.Infrastructure.Test.Provider;
using CAN.Webwinkel.Infrastructure.Test.RepositoriesTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CAN.Webwinkel.Infrastructure.Test.ServiceTests
{
    [TestClass]
    public class ArtikelServiceTests
    {
        private DbContextOptions<WinkelDatabaseContext> _options;
        private static ArtikelService service;
        private WinkelDatabaseContext _context;
        private static ArtikelRepository _repo;
        /// <summary>
        /// 
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            _options = TestDatabaseProvider.CreateInMemoryDatabaseOptions();

            _context = new WinkelDatabaseContext(_options);
            _repo = new ArtikelRepository(_context);
            ILogger<ArtikelService> logger = null;
            service = new ArtikelService(logger, _repo);
        }

        [TestMethod]
        public static void ArtikelenBijCategorieTest()
        {
            //Arrange
            var demo = new DemoEntities();
            var herenFiets = demo.HerenFiets;


            _repo.Insert(demo.HerenFiets);
            _repo.Insert(demo.DamesFiets);
            _repo.Insert(demo.Fiets);


            //Act
            var artikelen = service.ArtikelenBijCategorie("Heren fiets").ToArray();

            //Assert
            var herenfiets = artikelen[0];
            var fiets = artikelen[1];

            Assert.AreEqual(demo.HerenFiets.Id, herenfiets.Id);
            Assert.AreEqual(demo.Fiets.Id, fiets.Id);

        }

        [TestMethod]
        public static void AlleArtikelenTest()
        {
            //Arrange
            var demo = new DemoEntities();
            var herenFiets = demo.HerenFiets;

            _repo.Insert(demo.HerenFiets);
            _repo.Insert(demo.DamesFiets);
            _repo.Insert(demo.Fiets);


            //Act
            var alleArtikelen = service.AlleArtikelen();

            //Assert
            var artikelenLength = alleArtikelen.Count();

            Assert.AreEqual(3, artikelenLength);

        }

        [TestMethod]
        public static void AlleArtikelenPerPaginaOnevenAantalTest()
        {
            //Arrange
            var demo = new DemoEntities();
            var customHerenFiets = demo.HerenFiets;
            customHerenFiets.Id = 4;

            var customDamesFiets = demo.DamesFiets;
            customDamesFiets.Id = 5;


            //Dubble Inserts Only for testing.
            _repo.Insert(demo.HerenFiets);
            _repo.Insert(demo.DamesFiets);
            _repo.Insert(demo.Fiets);

            _repo.Insert(customHerenFiets);
            _repo.Insert(customDamesFiets);


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

        [TestMethod]
        public static void AantalPaginasTest()
        {
            //Arrange
            var demo = new DemoEntities();
            var customHerenFiets = demo.HerenFiets;
            customHerenFiets.Id = 4;

            var customDamesFiets = demo.DamesFiets;
            customDamesFiets.Id = 5;

            //Dubble Inserts Only for testing.
            _repo.Insert(demo.HerenFiets);
            _repo.Insert(demo.DamesFiets);
            _repo.Insert(demo.Fiets);

            _repo.Insert(customHerenFiets);
            _repo.Insert(customDamesFiets);


            //Act
            var aantalArtikelenPerPagina = 4;
            var aantalPaginas = service.AantalPaginas(aantalArtikelenPerPagina);

            //Assert
            var expectedAantalPaginas = 2;

            Assert.AreEqual(expectedAantalPaginas, aantalPaginas);
        }

        [ClassCleanup]
        public void Dispose()
        {
            _context.Dispose();
            _repo.Dispose();
        }
    }
}
