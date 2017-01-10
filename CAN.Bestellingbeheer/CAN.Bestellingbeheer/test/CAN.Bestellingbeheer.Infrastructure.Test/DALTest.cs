using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CAN.Bestellingbeheer.Domain.Domain.Entities;
using CAN.Bestellingbeheer.Infrastructure.DAL;
using CAN.Bestellingbeheer.Infrastructure.Infrastructure.Repositories;
using System.Collections;
using CAN.Bestellingbeheer.Domain.Entities;

namespace CAN.Bestellingbeheer.Infrastructure.Test.Test
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

            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                repo.Insert(new Bestelling()
                {
                    Artikelen = new List<Artikel>
                    {
                        new Artikel { Prijs = 2.50M }
                    }
                });
            }


            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                Assert.AreEqual(1, repo.Count());
            }
        }

        [TestMethod]
        public void TestFind()
        {
            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                repo.Insert(new Bestelling()
                {
                    Artikelen = new List<Artikel>
                    {
                        new Artikel { Prijs = 2.50M }
                    }
                });
            }

            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                var result = repo.Find(1);
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual(2.50M, result.Artikelen.First().Prijs);
            }
        }
        [TestMethod]
        public void TestDelete()
        {
            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                var bestelling = new Bestelling()
                {
                    Artikelen = new List<Artikel>
                    {
                        new Artikel { Prijs = 2.50M }
                    }
                };
                repo.Insert(bestelling);
                repo.Delete(1);
            }

            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                Assert.AreEqual(0, repo.Count());
            }
        }
        [TestMethod]
        public void TestFindAll()
        {
            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                var bestelling = new Bestelling()
                {
                    Artikelen = new List<Artikel>
                    {
                        new Artikel { Prijs = 2.50M }
                    }
                };
                repo.Insert(bestelling);
                bestelling = new Bestelling()
                {
                    Artikelen = new List<Artikel>
                    {
                        new Artikel { Prijs = 3.50M }
                    }
                };
                repo.Insert(bestelling);
            }

            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                Assert.AreEqual(2, repo.Count());
            }
        }
        [TestMethod]
        public void TestUpdate()
        {
            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                var bestelling = new Bestelling()
                {
                    Artikelen = new List<Artikel>
                    {
                        new Artikel { Prijs = 2.50M }
                    }
                };
                repo.Insert(bestelling);
                bestelling = repo.Find(1);
                bestelling.Artikelen.First().Prijs = 3.50M;
                repo.Update(bestelling);
            }

            using (var repo = new BestellingRepository(new DatabaseContext(_options)))
            {
                var bestelling = repo.Find(1);
                Assert.AreEqual(1, bestelling.Id);
                Assert.AreEqual(3.50M, bestelling.Artikelen.First().Prijs);
            }
        }
    }
}
