using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client.Events;
using CAN.Bestellingbeheer.Infrastructure.DAL;
using CAN.Common.Events;
using CAN.Bestellingbeheer.Infrastructure.Services;
using CAN.Bestellingbeheer.Infrastructure.Repositories;
using CAN.Bestellingbeheer.Domain.Entities;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CAN.Bestellingbeheer.Infrastructure.EventListener.Dispatchers
{
    public class WinkelMandjeEventDispatcher : EventDispatcher
    {
        private DbContextOptions<DatabaseContext> _dbOptions;
        private ILogger<WinkelMandjeEventDispatcher> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="dbOptions"></param>
        /// <param name="logger"></param>
        public WinkelMandjeEventDispatcher(BusOptions options, DbContextOptions<DatabaseContext> dbOptions, ILogger<WinkelMandjeEventDispatcher> logger) : base(options)
        {
            _logger = logger;
            _dbOptions = dbOptions;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void EventReceived(object sender, BasicDeliverEventArgs e)
        {
            base.EventReceived(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            if (Channel == null)
            {
                return false;
            }
            return Channel.IsOpen;
        }

        public void WinkelMandjeAfgerond(WinkelmandjeAfgerondEvent evt)
        {
            _logger.LogDebug($"Winkelmandje afgerond {evt.Timestamp} {evt.WinkelmandjeNummer}");

            var logger = new LoggerFactory()
                .AddSerilog(Log.Logger)
                .CreateLogger<BestellingService>();

            using (var publisher = new EventPublisher(base.BusOptions))
            using (var context = new DatabaseContext(_dbOptions))
            using (var repository = new BestellingRepository(context))
            using (var service = new BestellingService(publisher, repository, logger))
            {
                Bestelling bestelling = new Bestelling(evt);
                service.CreateBestelling(bestelling);
            }
        }
   
    }
}
