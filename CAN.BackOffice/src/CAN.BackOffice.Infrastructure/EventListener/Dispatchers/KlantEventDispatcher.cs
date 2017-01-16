using CAN.BackOffice.Domain.Entities;
using CAN.BackOffice.Infrastructure.DAL;
using CAN.BackOffice.Infrastructure.DAL.Repositories;
using CAN.Common.Events;
using CAN.Webwinkel.Infrastructure.EventListener;
using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client.Events;
using Serilog;

namespace CAN.BackOffice.Infrastructure.EventListener.Dispatchers
{
    public class KlantEventDispatcher : EventDispatcher
    {
        private DbContextOptions<DatabaseContext> _dbOptions;
        private ILogger _logger;
        private EventListenerLock _locker;
        public KlantEventDispatcher(BusOptions options, DbContextOptions<DatabaseContext> dbOptions, ILogger logger) : base(options)
        {
            _logger = logger;
            _dbOptions = dbOptions;
        }

        public KlantEventDispatcher(BusOptions options, DbContextOptions<DatabaseContext> dbOptions, ILogger logger, EventListenerLock locker) : base(options)
        {
            _logger = logger;
            _dbOptions = dbOptions;
            _locker = locker;
        }


        public void KlantAangemaakt(KlantCreatedEvent evt)
        {
            _logger.Debug($"Klant aangemaakt {evt.Klantnummer}");
            using (var context = new DatabaseContext(_dbOptions))
            using (var repo = new KlantRepository(context))
            {
                var klant = new Klant(evt);
                repo.Insert(klant);
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
            if (Channel == null)
            {
                return false;
            }
            return Channel.IsOpen;
        }
    }
}
