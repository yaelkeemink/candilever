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
using CAN.Webwinkel.Infrastructure.DAL.Entities;
using Serilog;

namespace CAN.Webwinkel.Infrastructure.EventListener.Dispatchers
{
    public class ArtikelEventDispatcher : EventDispatcher
    {
        private DbContextOptions<WinkelDatabaseContext> _dbOptions;
        private ILogger _logger;
        public ArtikelEventDispatcher(BusOptions options, DbContextOptions<WinkelDatabaseContext> dbOptions, ILogger logger) : base(options)
        {
            _logger = logger;
            _dbOptions = dbOptions;
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

        public void ArtikelUitCatalogusVerwijderd(ArtikelUitCatalogusVerwijderd evt)
        {
            _logger.Debug($"Artikel verwijdert {evt.Artikelnummer}");
            using (var context = new WinkelDatabaseContext(_dbOptions))
            using (var repo = new ArtikelRepository(context))
            {
                repo.Delete(evt.Artikelnummer);

            }
        }

        public void ArtikelInMagazijnGezet(ArtikelInMagazijnGezet evt)
        {
            _logger.Debug($"Artikel in magazijn {evt.ArtikelID} nieuwe voorraad {evt.Voorraad}");
            using (var context = new WinkelDatabaseContext(_dbOptions))
            using (var repo = new ArtikelRepository(context))
            {
               var artikel = repo.Find(evt.ArtikelID);
                artikel.Voorraad = evt.Voorraad;
                repo.Update(artikel);
            }

        }
        public void ArtikelInMagazijnGezet(ArtikelUitMagazijnGehaald evt)
        {
            _logger.Debug($"Artikel uit magazijn {evt.ArtikelID} nieuwe voorraad {evt.Voorraad}");
            using (var context = new WinkelDatabaseContext(_dbOptions))
            using (var repo = new ArtikelRepository(context))
            {
                var artikel = repo.Find(evt.ArtikelID);
                artikel.Voorraad = evt.Voorraad;
                repo.Update(artikel);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            if(Channel == null)
            {
                _logger.Information("Channel not set");
            }
            return Channel.IsOpen;
        }
    }
}
