using CAN.Webwinkel.Infrastructure.DAL;
using CAN.Webwinkel.Infrastructure.EventListener;
using CAN.Webwinkel.Infrastructure.Test.Provider;
using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Infrastructure.Test.EventListenerTest
{
    [TestClass]
    public class WinkelEventListenerTest
    {
        private BusOptions _busOptions;
        private string _dbConnectionString;
        private ILogger _logger;
        private string _replayEndPoint;
        private EventListenerLock _locker;

        [TestMethod]
        public void ReplayAuditLogTest()
        {
            var options = TestDatabaseProvider.CreateMsSQLDatabaseOptions();
            var context = new WinkelDatabaseContext(options);

            var listener = InitEventListener();
            listener.Start();
            /// wachten
            _locker.StartUpLock.WaitOne();

            Assert.AreEqual(1, 2);
        }

        private WinkelEventListener InitEventListener()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.LiterateConsole()
                .WriteTo.RollingFile("logs\\ReplayAuditLogs-{Date}.txt")
                .CreateLogger();

            var _busOptions = BusOptions.CreateFromEnvironment();

            var replayBusOptions = new BusOptions
            {
                ExchangeName = $"Kantilever.ReplayExchange.{DateTime.Now.Millisecond}",
                QueueName = "ReplayService",
                HostName = "can-kantilever-eventbus",
                Port = 11998,
                UserName = "Kantilever",
                Password = "Kant1lever"
            };


            _dbConnectionString = "Server=.\\SQLEXPRESS; Database=WebshopTest; Trusted_Connection=True;";
            _locker = new EventListenerLock();
            var replayService = "ReplayService";
            var listener = new WinkelEventListener(replayBusOptions, _dbConnectionString, _logger, replayService, _locker);

            return listener;
        }
    }
}
