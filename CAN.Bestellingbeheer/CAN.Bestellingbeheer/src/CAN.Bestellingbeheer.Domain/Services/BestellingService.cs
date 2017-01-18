using CAN.Bestellingbeheer.Domain.Entities;
using CAN.Bestellingbeheer.Domain.Interfaces;
using CAN.Common.Events;
using InfoSupport.WSA.Infrastructure;
using System;
using Microsoft.Extensions.Logging;
using CAN.Bestellingbeheer.Domain.Exceptions;
using CAN.Bestellingbeheer.Domain.DTO;

namespace CAN.Bestellingbeheer.Domain.Services {
    public class BestellingService 
        : IBestellingService, IDisposable
    {
        private readonly IRepository<Bestelling, long> _repository;
        private readonly IEventPublisher _publisher;
        private readonly ILogger<BestellingService> _logger;

        public BestellingService(IEventPublisher publisher, IRepository<Bestelling, long> repository, ILogger<BestellingService> logger)
        {
            _publisher = publisher;
            _repository = repository;
            _logger = logger;
        }

        public BestellingDTO CreateBestelling(Bestelling bestelling)
        {
            long bestellingsnummer = _repository.Insert(bestelling);

            var createdEvent = new BestellingCreatedEvent("can.bestellingbeheer.bestellingcreated")
            {
                Klantnummer = bestelling.Klantnummer,
                Bestellingsnummer = bestellingsnummer,
                BestelDatum = bestelling.BestelDatum,
            };

            foreach (var artikel in bestelling.Artikelen)
            {
                createdEvent.AddArtikel(artikel.Artikelnummer, artikel.Naam, artikel.Prijs, artikel.Aantal, artikel.Leverancier, artikel.LeverancierCode);
            }

            _publisher.Publish(createdEvent);            
            return new BestellingDTO(bestelling);
        }

        public int UpdateBestelling(Bestelling bestelling)
        {
            return _repository.Update(bestelling);
        }

        public int StatusNaarOpgehaald(long id)
        {
            var bestelling = _repository.Find(id);
            if (bestelling.Status != BestelStatus.Opgehaald)
            {
                bestelling.Status = BestelStatus.Opgehaald;
                return _repository.Update(bestelling);
            }
            throw new InvalidStatusException("Status staat al op opgehaald");
        }
        public void Dispose()
        {
            _repository?.Dispose();
            _publisher?.Dispose();
        }
    }
}
