using CAN.Webwinkel.Infrastructure.DAL;
using CAN.Webwinkel.Infrastructure.EventListener.Dispatchers;
using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using InfoSupport.WSA.Logging.Model;

namespace CAN.Webwinkel.Infrastructure.EventListener
{
    public class WinkelEventListener
    {

        private BusOptions _busOptions;
        private string _dbConnectionString;
        private ILogger _logger;
        private string _replayEndPoint;

        public WinkelEventListener(BusOptions busOptions, string dbConnectionString, ILogger logger, string replayEndPoint)
        {
            _busOptions = busOptions;
            _dbConnectionString = dbConnectionString;
            _logger = logger;
            _replayEndPoint = replayEndPoint;
        }


        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            var thread = new Thread(new ThreadStart(Run));
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Run()
        {
            var builder = new DbContextOptionsBuilder<WinkelDatabaseContext>();
            builder.UseSqlServer(_dbConnectionString);
            var dbOptions = builder.Options;



            while (true)
            {

                try
                {
                    using (var dispatcher = new ArtikelEventDispatcher(_busOptions, dbOptions, _logger))
                    {

                        _logger.Information("Start rebuilding cache");
                        ReplayAuditlog(dbOptions);
                        _logger.Information("Done rebuilding cache");


                        _logger.Debug("Opening connection with Rabbit mq");
                        dispatcher.Open();
                        _logger.Debug("Connection with Rabbit mq is open");
                        while (dispatcher.IsConnected())
                        {
                            _logger.Information("Connected with Rabbit Mq is stil open");
                            Thread.Sleep(60000);
                        }
                        _logger.Information("Connection with Rabbit Mq lost");
                    }
                }
                catch (Exception e)
                {
                    _logger.Error($"Error with EventDispatcher {e.Message}");
                    Thread.Sleep(5000);
                }
            }
        }

        private void ReplayAuditlog(DbContextOptions<WinkelDatabaseContext> dbOptions)
        {
            _logger.Information("Purging database");
            using (var context = new WinkelDatabaseContext(dbOptions))
            {
                context.PurgeCachedData();
            }
            _logger.Information("Purge complete");


            var replayBusOptions = new BusOptions
            {
                ExchangeName = $"Kantilever.Voorbeeld.ReplayExchange.{DateTime.Now.Millisecond}",
                QueueName = "TempQueue",
                HostName = _busOptions.HostName,
                Port = _busOptions.Port,
                UserName = _busOptions.UserName,
                Password = _busOptions.Password
            };

            using (var listener = new ArtikelEventDispatcher(replayBusOptions, dbOptions, _logger))
            using (var auditlogproxy = new MicroserviceProxy(_replayEndPoint, _busOptions))
            {
                listener.Open();

                var replayCommand = new ReplayEventsCommand
                {
                    ExchangeName = replayBusOptions.ExchangeName,
                    //RoutingKeyExpression = "Kantilever.#",
                };

                _logger.Information($"Start replaying Events on Exchange={replayCommand.ExchangeName}...");
                auditlogproxy.Execute(replayCommand);
                _logger.Information("Done replaying events.");
                Thread.Sleep(60000);
            }


        }
    }
}
