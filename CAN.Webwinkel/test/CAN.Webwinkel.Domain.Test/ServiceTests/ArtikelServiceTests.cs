

using CAN.Webwinkel.Infrastructure.DAL;
using CAN.Webwinkel.Infrastructure.DAL.Repositories;
using CAN.Webwinkel.Infrastructure.Services;
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
        private static ArtikelService _service;
        private WinkelDatabaseContext _context;
        private static ArtikelRepository _repo;

        [TestInitialize]
        public void Init()
        {
            _options = TestDatabaseProvider.CreateInMemoryDatabaseOptions();

            _context = new WinkelDatabaseContext(_options);
            _repo = new ArtikelRepository(_context);

            ILogger<ArtikelService> logger = null;

            _service = new ArtikelService(logger, _repo);
        }

        [TestMethod]
        public void AlleArtikelenTest()
        {
            //Arrange
            var demo = new DemoEntities();
            var herenFiets = demo.HerenFiets;

            _repo.Insert(demo.HerenFiets);
            _repo.Insert(demo.DamesFiets);
            _repo.Insert(demo.Fiets);


            //Act
            var alleArtikelen = _service.AlleArtikelen();

            //Assert
            var artikelenLength = alleArtikelen.Count();

            Assert.AreEqual(3, artikelenLength);

        }

        [TestMethod]
        public void AlleArtikelenPerPaginaOnevenAantalTest()
        {
            //Arrange
            var demo = new DemoEntities();
            var customHerenFiets = demo.HerenFiets;

            var customDamesFiets = demo.DamesFiets;

            //Dubble Inserts Only for testing.
            _repo.Insert(demo.HerenFiets);
            _repo.Insert(demo.DamesFiets);
            _repo.Insert(demo.Fiets);

            _repo.Insert(customHerenFiets);
            _repo.Insert(customDamesFiets);


            //Act
            var paginaNummer = 2;
            var aantalArtikelen = 3;
            var alleArtikelenOpPagina = _service.AlleArtikelenPerPagina(paginaNummer, aantalArtikelen).ToArray();

            //Assert
            var eersteArtikelOpPagina = alleArtikelenOpPagina[0];
            var tweedeArtikelOpPagina = alleArtikelenOpPagina[1];

            var artikelenLength = alleArtikelenOpPagina.Count();

            Assert.AreEqual(customHerenFiets.Artikelnummer, eersteArtikelOpPagina.Artikelnummer);
            Assert.AreEqual(customDamesFiets.Artikelnummer, tweedeArtikelOpPagina.Artikelnummer);
            Assert.AreEqual(2, artikelenLength); // Er zijn 5 inserts gedaan er er worden 3 artikelen overgeslagen.

        }

        [TestMethod]
        public void AantalPaginasTest()
        {
            //Arrange
            var demo = new DemoEntities();
            var customHerenFiets = demo.HerenFiets;

            var customDamesFiets = demo.DamesFiets;

            //Dubble Inserts Only for testing.
            _repo.Insert(demo.HerenFiets);
            _repo.Insert(demo.DamesFiets);
            _repo.Insert(demo.Fiets);

            _repo.Insert(customHerenFiets);
            _repo.Insert(customDamesFiets);


            //Act
            var aantalArtikelenPerPagina = 4;
            var aantalPaginas = _service.AantalPaginas(aantalArtikelenPerPagina);

            //Assert
            var expectedAantalPaginas = 2;

            Assert.AreEqual(expectedAantalPaginas, aantalPaginas);
        }

        [TestCleanup]
        public void Dispose()
        {
            _context.Dispose();
            _repo.Dispose();
            _service.Dispose();
        }
    }
}
