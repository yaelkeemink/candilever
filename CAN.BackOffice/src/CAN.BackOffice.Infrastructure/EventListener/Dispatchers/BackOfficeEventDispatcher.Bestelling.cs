using Can.BackOffice.Domain.Entities;
using CAN.BackOffice.Infrastructure.DAL;
using CAN.BackOffice.Infrastructure.DAL.Repositories;
using CAN.Common.Events;
using InfoSupport.WSA.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CAN.BackOffice.Infrastructure.EventListener.Dispatchers
{
    public partial class BackOfficeEventDispatcher
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
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


    }
}
