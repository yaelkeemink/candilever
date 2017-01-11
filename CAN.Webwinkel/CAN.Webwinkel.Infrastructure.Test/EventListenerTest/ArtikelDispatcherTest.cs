using CAN.Webwinkel.Infrastructure.Test.Provider;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Infrastructure.Test.EventListenerTest
{
    [TestClass]
    public class ArtikelDispatcherTest
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

        public void ArtikelToegevoegdAanCatalogus()
        {

        }


    }
}
