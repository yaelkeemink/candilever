﻿using CAN.BackOffice.Domain.Entities;
using CAN.BackOffice.Infrastructure.DAL;
using CAN.BackOffice.Infrastructure.DAL.Repositories;
using CAN.Common.Events;


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

        public void BestellingStatusUpdated(BestellingStatusUpdatedEvent evt)
        {
            _logger.Information($"Bestelling Geupdated {evt.BestellingsNummer} {evt.BestellingStatusCode}");
            using (var context = new DatabaseContext(_dbOptions))
            using (var repo = new BestellingRepository(context))
            {
                var bestelling = repo.Find(evt.BestellingsNummer);
                bestelling.BestellingStatusCode = evt.BestellingStatusCode;
                repo.Update(bestelling);
            }
        }
    }
}
