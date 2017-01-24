using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using Serilog;
using CAN.Bestellingbeheer.Infrastructure.DAL;
using CAN.Bestellingbeheer.Infrastructure.EventListener.Dispatchers;

namespace CAN.Bestellingbeheer.Infrastructure.EventListener
{
    public class BestellingbeheerEventListener
    {

        private BusOptions _busOptions;
        private string _dbConnectionString;
        private ILogger _logger;
        private string _replayEndPoint;
        public BestellingbeheerEventListener(BusOptions busOptions, string dbConnectionString, ILogger logger, string replayEndPoint)
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
            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.UseSqlServer(_dbConnectionString);
            var dbOptions = builder.Options;

            while (true)
            {
                try
                {
                    using (var dispatcher = new WinkelMandjeEventDispatcher(_busOptions, dbOptions, _logger))
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
                    _logger.Debug(e.StackTrace);
                    Thread.Sleep(5000);
                }
            }
        }
        
    }
}
