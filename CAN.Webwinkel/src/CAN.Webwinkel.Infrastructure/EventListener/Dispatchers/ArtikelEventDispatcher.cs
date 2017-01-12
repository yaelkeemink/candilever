using InfoSupport.WSA.Infrastructure;
using Kantilever.Catalogusbeheer.Events;
using Kantilever.Magazijnbeheer;
using Kantilever.Magazijnbeheer.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAN.Webwinkel.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using CAN.Webwinkel.Infrastructure.DAL.Repositories;
using CAN.Webwinkel.Domain.Entities;
using Serilog;
using RabbitMQ.Client.Events;

namespace CAN.Webwinkel.Infrastructure.EventListener.Dispatchers
{
    public class ArtikelEventDispatcher : EventDispatcher
    {
        private DbContextOptions<WinkelDatabaseContext> _dbOptions;
        private ILogger _logger;
        private EventListenerLock _locker;
        public ArtikelEventDispatcher(BusOptions options, DbContextOptions<WinkelDatabaseContext> dbOptions, ILogger logger) : base(options)
        {
            _logger = logger;
            _dbOptions = dbOptions;
        }

        public ArtikelEventDispatcher(BusOptions options, DbContextOptions<WinkelDatabaseContext> dbOptions, ILogger logger, EventListenerLock locker) : base(options)
        {
            _logger = logger;
            _dbOptions = dbOptions;
            _locker = locker;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
        public void ArtikelAanCatalogusToegevoegd(ArtikelAanCatalogusToegevoegd evt)
        {          
            _logger.Debug($"Artikel toegevoegd {evt.Artikelnummer} {evt.Naam}");
            using (var context = new WinkelDatabaseContext(_dbOptions))
            using (var repo = new ArtikelRepository(context))
            {
                var artikel = new Artikel(evt);
                repo.Insert(artikel);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
        public void ArtikelUitCatalogusVerwijderd(ArtikelUitCatalogusVerwijderd evt)
        {
            _logger.Debug($"Artikel verwijdert {evt.Artikelnummer}");
            using (var context = new WinkelDatabaseContext(_dbOptions))
            using (var repo = new ArtikelRepository(context))
            {
                repo.Delete(evt.Artikelnummer);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
        public void ArtikelInMagazijnGezet(ArtikelInMagazijnGezet evt)
        {
            _logger.Debug($"Artikel in magazijn {evt.ArtikelID} nieuwe voorraad {evt.Voorraad}");
            UpdateArtikelVoorraad(evt.ArtikelID, evt.Voorraad);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
        public void ArtikelUitMagazijnGehaald(ArtikelUitMagazijnGehaald evt)
        {
            _logger.Debug($"Artikel uit magazijn {evt.ArtikelID} nieuwe voorraad {evt.Voorraad}");
            UpdateArtikelVoorraad(evt.ArtikelID, evt.Voorraad);
        }


        private void UpdateArtikelVoorraad(int artikelNummer, int nieuweVoorrraad)
        {
            using (var context = new WinkelDatabaseContext(_dbOptions))
            using (var repo = new ArtikelRepository(context))
            {
                var artikel = repo.Find(artikelNummer);
                artikel.Voorraad = nieuweVoorrraad;
                repo.Update(artikel);
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
