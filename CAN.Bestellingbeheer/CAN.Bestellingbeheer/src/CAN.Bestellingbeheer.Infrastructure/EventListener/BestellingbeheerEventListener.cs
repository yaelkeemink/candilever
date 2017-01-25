using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using CAN.Bestellingbeheer.Infrastructure.DAL;
using CAN.Bestellingbeheer.Infrastructure.EventListener.Dispatchers;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CAN.Bestellingbeheer.Infrastructure.EventListener
{
    public class BestellingbeheerEventListener
    {

        private BusOptions _busOptions;
        private string _dbConnectionString;
        private ILogger<BestellingbeheerEventListener> _logger;
        private string _replayEndPoint;
        public BestellingbeheerEventListener(BusOptions busOptions, string dbConnectionString, ILogger<BestellingbeheerEventListener> logger, string replayEndPoint)
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
                    var logger = new LoggerFactory()
                        .AddSerilog(Log.Logger)
                        .CreateLogger<WinkelMandjeEventDispatcher>();

                    using (var dispatcher = new WinkelMandjeEventDispatcher(_busOptions, dbOptions, logger))
                    {
                        _logger.LogDebug("Opening connection with Rabbit mq");
                        dispatcher.Open();

                        _logger.LogDebug("Connection with Rabbit mq is open");

                        while (dispatcher.IsConnected())
                        {
                            _logger.LogInformation("Connected with Rabbit Mq is stil open");
                            Thread.Sleep(60000);
                        }
                        _logger.LogInformation("Connection with Rabbit Mq lost");
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error with EventDispatcher {e.Message}");
                    _logger.LogDebug(e.StackTrace);
                    Thread.Sleep(5000);
                }
            }
        }
        
    }
}
