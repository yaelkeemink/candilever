using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using RabbitMQ.Client.Events;
using CAN.BackOffice.Infrastructure.DAL;
using CAN.Webwinkel.Infrastructure.EventListener;
using CAN.Common.Events;
using CAN.BackOffice.Infrastructure.DAL.Repositories;
using CAN.BackOffice.Domain.Entities;
using System;
using System.Threading;
using System.Linq;

namespace CAN.BackOffice.Infrastructure.EventListener.Dispatchers
{
    public class BackOfficeEventDispatcher : EventDispatcher
    {
        private DbContextOptions<DatabaseContext> _dbOptions;
        private ILogger _logger;
        private EventListenerLock _locker;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="dbOptions"></param>
        /// <param name="logger"></param>
        public BackOfficeEventDispatcher(BusOptions options, DbContextOptions<DatabaseContext> dbOptions, ILogger logger) : base(options)
        {
            _logger = logger;
            _dbOptions = dbOptions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="dbOptions"></param>
        /// <param name="logger"></param>
        /// <param name="locker"></param>
        public BackOfficeEventDispatcher(BusOptions options, DbContextOptions<DatabaseContext> dbOptions, ILogger logger, EventListenerLock locker) : base(options)
        {
            _logger = logger;
            _dbOptions = dbOptions;
            _locker = locker;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void EventReceived(object sender, BasicDeliverEventArgs e)
        {
            _logger.Debug($"Event received");
            if (_locker != null)
            {
                _locker.EventReceived();
            }

            base.EventReceived(sender, e);
        }

        public void BestellingAangemaakt(BestellingCreatedEvent evt)
        {
            _logger.Information($"Bestelling aangemaakt {evt.Bestellingsnummer} {evt.BestelDatum} {evt.Klantnummer}");
            using (var context = new DatabaseContext(_dbOptions))
            using (var repo = new BestellingRepository(context))
            {
                var bestelling = new Bestelling(evt);
                repo.Insert(bestelling);
            }
        }

        public void BestellingStatusUpdated(BestellingStatusUpdatedEvent evt)
        {
            _logger.Information($"Bestelling Geupdated {evt.BestellingsNummer} {evt.BestellingStatusCode}");
            using (var context = new DatabaseContext(_dbOptions))
            using (var repo = new BestellingRepository(context))
            {
                var bestelling = repo.FindBy(b => b.Bestellingsnummer == evt.BestellingsNummer).Single(); ;
                bestelling.BestellingStatusCode = evt.BestellingStatusCode;
                repo.Update(bestelling);
            }
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
