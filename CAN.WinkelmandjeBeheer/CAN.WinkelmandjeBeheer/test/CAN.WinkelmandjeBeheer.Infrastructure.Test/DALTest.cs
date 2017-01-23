using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CAN.WinkelmandjeBeheer.Infrastructure.DAL;
using CAN.WinkelmandjeBeheer.Infrastructure.Infrastructure.Repositories;
using CAN.WinkelmandjeBeheer.Domain.Domain.Entities;
using CAN.WinkelmandjeBeheer.Domain.DTO;

namespace CAN.WinkelmandjeBeheer.Infrastructure.Test.Test
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

            using (var repo = new WinkelmandjeRepository(new DatabaseContext(_options)))
            {
                repo.Insert(new Winkelmandje());
            }


            using (var repo = new WinkelmandjeRepository(new DatabaseContext(_options)))
            {
                Assert.AreEqual(1, repo.Count());
            }
        }

        [TestMethod]
        public void TestFind()
        {
            var guid = Guid.NewGuid();
            using (var repo = new WinkelmandjeRepository(new DatabaseContext(_options)))
            {
                repo.Insert(new Winkelmandje()
                {
                    WinkelmandjeNummer = guid
                });
            }

            using (var repo = new WinkelmandjeRepository(new DatabaseContext(_options)))
            {
                var result = repo.Find(guid);
                Assert.AreEqual(guid, result.WinkelmandjeNummer);
            }
        }
        [TestMethod]
        public void TestDelete()
        {
            var guid = Guid.NewGuid();
            using (var repo = new WinkelmandjeRepository(new DatabaseContext(_options)))
            {
                var winkelmandje = new Winkelmandje()
                {
                    WinkelmandjeNummer = guid
                };
                repo.Insert(winkelmandje);
                repo.Delete(guid);
            }

            using (var repo = new WinkelmandjeRepository(new DatabaseContext(_options)))
            {
                Assert.AreEqual(0, repo.Count());
            }
        }
        [TestMethod]
        public void TestFindAll()
        {
            var guid = Guid.NewGuid();
            using (var repo = new WinkelmandjeRepository(new DatabaseContext(_options)))
            {
                var winkelmandje = new Winkelmandje()
                {
                    WinkelmandjeNummer = guid
                };
                repo.Insert(winkelmandje);
                winkelmandje = new Winkelmandje();
                repo.Insert(winkelmandje);
            }

            using (var repo = new WinkelmandjeRepository(new DatabaseContext(_options)))
            {
                Assert.AreEqual(2, repo.Count());
            }
        }
        [TestMethod]
        public void TestUpdate()
        {
            var guid = Guid.NewGuid();
            var updatedGuid = Guid.NewGuid();
            using (var repo = new WinkelmandjeRepository(new DatabaseContext(_options)))
            {
                var winkelmandje = new Winkelmandje()
                {
                    WinkelmandjeNummer = guid
                };
                repo.Insert(winkelmandje);
                winkelmandje = repo.Find(guid);
                winkelmandje.WinkelmandjeNummer = updatedGuid;
                repo.Update(winkelmandje);
            }

            using (var repo = new WinkelmandjeRepository(new DatabaseContext(_options)))
            {
                var winkelmandje = repo.Find(updatedGuid);
                Assert.AreEqual(updatedGuid, winkelmandje.WinkelmandjeNummer);
            }
        }
    }
}
