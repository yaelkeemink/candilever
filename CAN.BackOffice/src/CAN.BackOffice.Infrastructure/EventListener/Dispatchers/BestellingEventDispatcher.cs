using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using RabbitMQ.Client.Events;
using CAN.BackOffice.Infrastructure.DAL;
using CAN.Common.Events;
using CAN.BackOffice.Infrastructure.DAL.Repositories;
using Can.BackOffice.Domain.Entities;

namespace CAN.Webwinkel.Infrastructure.EventListener.Dispatchers
{
    public class BestellingEventDispatcher : EventDispatcher
    {
        private DbContextOptions<DatabaseContext> _dbOptions;
        private ILogger _logger;
        private EventListenerLock _locker;
        public BestellingEventDispatcher(BusOptions options, DbContextOptions<DatabaseContext> dbOptions, ILogger logger) : base(options)
        {
            _logger = logger;
            _dbOptions = dbOptions;
        }

        public BestellingEventDispatcher(BusOptions options, DbContextOptions<DatabaseContext> dbOptions, ILogger logger, EventListenerLock locker) : base(options)
        {
            _logger = logger;
            _dbOptions = dbOptions;
            _locker = locker;
        }


        public void BestellingAangemaakt(BestellingCreatedEvent evt)
        {
            _logger.Debug($"Bestelling aangemaakt {evt.Bestellingsnummer} {evt.BestelDatum} {evt.Klantnummer}");
            using (var context = new DatabaseContext(_dbOptions))
            using (var repo = new BestellingRepository(context))
            {
                var bestelling = new Bestelling(evt);
                repo.Insert(bestelling);
            }
        }
        
        protected override void EventReceived(object sender, BasicDeliverEventArgs e)
        {
            if (_locker != null)
            {
                _locker.EventReceived();
            }

            base.EventReceived(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            if(Channel == null)
            {
                return false;
            }
            return Channel.IsOpen;
        }
    }
}
