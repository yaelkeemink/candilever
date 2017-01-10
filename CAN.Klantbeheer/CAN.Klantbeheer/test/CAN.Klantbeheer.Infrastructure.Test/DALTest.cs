using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CAN.Klantbeheer.Infrastructure.DAL;
using CAN.Klantbeheer.Infrastructure.Infrastructure.Repositories;
using CAN.Klantbeheer.Domain.Domain.Entities;

namespace CAN.Klantbeheer.Infrastructure.Test.Test
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

            using (var repo = new PlayerRepository(new DatabaseContext(_options)))
            {
                repo.Insert(new Klant()
                {
                    Name = "Naam"
                });
            }


            using (var repo = new PlayerRepository(new DatabaseContext(_options)))
            {
                Assert.AreEqual(1, repo.Count());
            }
        }

        [TestMethod]
        public void TestFind()
        {
            using (var repo = new PlayerRepository(new DatabaseContext(_options)))
            {
                repo.Insert(new Klant()
                {
                    Name = "Name"
                });
            }

            using (var repo = new PlayerRepository(new DatabaseContext(_options)))
            {
                var result = repo.Find(1);
                Assert.AreEqual(1, result.Klantnummer);
                Assert.AreEqual("Name", result.Name);
            }
        }
        [TestMethod]
        public void TestDelete()
        {
            using (var repo = new PlayerRepository(new DatabaseContext(_options)))
            {
                var player = new Klant()
                {
                    Name = "Name"
                };
                repo.Insert(player);
                repo.Delete(1);
            }

            using (var repo = new PlayerRepository(new DatabaseContext(_options)))
            {
                Assert.AreEqual(0, repo.Count());
            }
        }
        [TestMethod]
        public void TestFindAll()
        {
            using (var repo = new PlayerRepository(new DatabaseContext(_options)))
            {
                var player = new Klant()
                {
                    Name = "Entity"
                };
                repo.Insert(player);
                player = new Klant()
                {
                    Name = "Name"
                };
                repo.Insert(player);
            }

            using (var repo = new PlayerRepository(new DatabaseContext(_options)))
            {
                Assert.AreEqual(2, repo.Count());
            }
        }
        [TestMethod]
        public void TestUpdate()
        {
            using (var repo = new PlayerRepository(new DatabaseContext(_options)))
            {
                var player = new Klant()
                {
                    Name = "Entity"
                };
                repo.Insert(player);
                player = repo.Find(1);
                player.Name = "UpdatedName";
                repo.Update(player);
            }

            using (var repo = new PlayerRepository(new DatabaseContext(_options)))
            {
                var player = repo.Find(1);
                Assert.AreEqual(1, player.Klantnummer);
                Assert.AreEqual("UpdatedName", player.Name);
            }
        }
    }
}
