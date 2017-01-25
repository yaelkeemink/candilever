using CAN.Webwinkel.Domain.Entities;
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

namespace CAN.Webwinkel.Domain.Test.ServiceTests
{
    [TestClass]
    public class CategorieServiceTests
    {
        private DbContextOptions<WinkelDatabaseContext> _options;
        private static CategorieService service;
        private WinkelDatabaseContext _context;
        private static CategorieRepository _repo;
        /// <summary>
        /// 
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            _options = TestDatabaseProvider.CreateInMemoryDatabaseOptions();

            _context = new WinkelDatabaseContext(_options);
            _repo = new CategorieRepository(_context);
            ILogger<CategorieService> logger = null;
            service = new CategorieService(logger, _repo);
        }

        [TestMethod]
        public static void AlleCategroieenTest()
        {
            //Arrange
            DemoEntities demo = new DemoEntities();

            var categorie = demo.Categorie();
            var categorie_2 = demo.Categorie_2();

            _repo.Insert(categorie);
            _repo.Insert(categorie_2);

            //Act
            var alleCategorieen = service.AlleCategorieen();

            //Assert
            Assert.AreEqual(2, alleCategorieen.Count());
        }

        [ClassCleanup]
        public void Dispose()
        {
            _context.Dispose();
            _repo.Dispose();
        }
    }
}
