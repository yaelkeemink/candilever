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

namespace CAN.Webwinkel.Infrastructure.EventListener
{
    public class WinkelEventListener
    {

        private BusOptions _busOptions;
        private string _dbConnectionString;
        private ILogger _logger;


        public WinkelEventListener(BusOptions busOptions, string dbConnectionString, ILogger logger)
        {
            _busOptions = busOptions;
            _dbConnectionString = dbConnectionString;
            _logger = logger;
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

    }
}
