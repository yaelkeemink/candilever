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

namespace CAN.Webwinkel.Infrastructure.EventListener.Dispatchers
{
    public class ArtikelEventDispatcher : EventDispatcher
    {
        private DbContextOptions<WinkelDatabaseContext> _dbOptions;

        public ArtikelEventDispatcher(BusOptions options = null, DbContextOptions<WinkelDatabaseContext> dbOptions = null) : base(options)
        {
            _dbOptions = dbOptions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
        public void ArtikelAanCatalogusToegevoegd(ArtikelAanCatalogusToegevoegd evt)
        {
            using (var context = new WinkelDatabaseContext(_dbOptions))
            using (var repo = new ArtikelRepository(context))
            {
                var artikel = new Artikel(evt);
                repo.Insert(artikel);
                
            }
        }
        public void ArtikelUitCatalogusVerwijderd(ArtikelUitCatalogusVerwijderd evt)
        {
            Console.WriteLine(evt.GetType().Name);
            Console.WriteLine(evt.Artikelnummer);
        }
        public void ArtikelInMagazijnGezet(ArtikelInMagazijnGezet evt)
        {
            Console.WriteLine(evt.GetType().Name);
            Console.WriteLine($"nr={evt.ArtikelID}, voorraad={evt.Voorraad}");
        }
        public void ArtikelInMagazijnGezet(ArtikelUitMagazijnGehaald evt)
        {
            Console.WriteLine(evt.GetType().Name);
            Console.WriteLine($"nr={evt.ArtikelID}, voorraad={evt.Voorraad}");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IModel GetConnection()
        {
            return Channel;
        }
    }
}
