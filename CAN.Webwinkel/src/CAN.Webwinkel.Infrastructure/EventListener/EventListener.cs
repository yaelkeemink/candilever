using CAN.Webwinkel.Infrastructure.DAL;
using CAN.Webwinkel.Infrastructure.EventListener.Dispatchers;
using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Infrastructure.EventListener
{
    public class EventListener
    {

        private BusOptions _busOptions;
        private string _dbConnectionString;
        private ILogger<EventListener> _logger;


        public EventListener(BusOptions busOptions, string dbConnectionString, ILogger<EventListener> logger)
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
                    using (var dispatcher = new ArtikelEventDispatcher(_busOptions, dbOptions))
                    {
                        while (dispatcher.GetConnection().IsOpen)
                        {
                            Thread.Sleep(60000);
                        }
                        _logger.LogInformation("Connection with Rabbit Mq lost");
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("Error with EventDispatcher", e);
                    Thread.Sleep(5000);
                }
            }
        }

    }
}
