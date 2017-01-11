using CAN.Bestellingbeheer.Domain.Entities;
using CAN.Bestellingbeheer.Domain.Interfaces;
using CAN.Common.Events;
using InfoSupport.WSA.Infrastructure;
using System;
using Microsoft.Extensions.Logging;

namespace CAN.Bestellingbeheer.Domain.Services {
    public class BestellingService : IDisposable
    {
        private readonly IRepository<Bestelling, long> _repository;
        private readonly IEventPublisher _publisher;
        private readonly ILogger<BestellingService> _logger;

        public BestellingService(ILogger<BestellingService> logger, IEventPublisher publisher, IRepository<Bestelling, long> repository)
        {
            _logger = logger;
            _publisher = publisher;
            _repository = repository;
        }

        public Bestelling CreateBestelling(Bestelling bestelling)
         {
            try
            {
                long bestellingsnummer = _repository.Insert(bestelling);

                var createdEvent = new BestellingCreatedEvent("can.bestellingbeheer.bestellingcreated")
                {
                    Bestellingsnummer = bestellingsnummer,
                    BestelDatum = bestelling.BestelDatum,
                };

                foreach(var artikel in bestelling.Artikelen)
                {
                    createdEvent.AddArtikel(artikel.Id, artikel.Naam, artikel.Prijs, artikel.Aantal);
                }
                
                _publisher.Publish(createdEvent);

                _logger.LogInformation("Bestelling created.", bestelling);

            } catch(Exception e)
            {
                _logger.LogError("Bestelling created failed.", bestelling, e.Message);
            }

            return bestelling;
        }

        public int UpdateBestelling(Bestelling bestelling)
        {
            return _repository.Update(bestelling);
        }
        public void Dispose()
        {
            _repository?.Dispose();
            _publisher?.Dispose();
        }


    }
}
