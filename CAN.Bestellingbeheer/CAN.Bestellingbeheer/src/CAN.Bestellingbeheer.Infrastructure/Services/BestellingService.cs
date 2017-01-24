using CAN.Bestellingbeheer.Domain.Entities;
using CAN.Common.Events;
using InfoSupport.WSA.Infrastructure;
using System;
using CAN.Bestellingbeheer.Domain.Exceptions;
using CAN.Bestellingbeheer.Infrastructure.Interfaces;
using Serilog;

namespace CAN.Bestellingbeheer.Infrastructure.Services {
    public class BestellingService 
        : IBestellingService, IDisposable
    {
        private readonly IRepository<Bestelling, long> _repository;
        private readonly IEventPublisher _publisher;
        private readonly ILogger _logger;

        public BestellingService(IEventPublisher publisher, IRepository<Bestelling, long> repository, ILogger logger)
        {
            _publisher = publisher;
            _repository = repository;
            _logger = logger;
        }

        public void CreateBestelling(Bestelling bestelling)
        {
            long bestellingsnummer = _repository.Insert(bestelling);

            var createdEvent = new BestellingCreatedEvent("can.bestellingbeheer.bestellingcreated")
            {
                Klantnummer = bestelling.Klantnummer,
                Bestellingsnummer = bestellingsnummer,
                BestelDatum = bestelling.BestelDatum,
                BestellingStatusNummer = (int)bestelling.Status,
                BestellingStatusCode = bestelling.Status.ToString(),
                VolledigeNaam = bestelling.VolledigeNaam,
                Adres = bestelling.Adres,
                Huisnummer = bestelling.Huisnummer,
                Postcode = bestelling.Postcode,
                Land = bestelling.Land
            };

            foreach (var artikel in bestelling.Artikelen)
            {
                createdEvent.AddArtikel(
                    artikel.Artikelnummer, 
                    artikel.Naam, 
                    artikel.Prijs, 
                    artikel.Aantal, 
                    artikel.LeverancierCode, 
                    artikel.Leverancier);
            }

            _publisher.Publish(createdEvent);
        }

        public int UpdateBestelling(Bestelling bestelling)
        {
            return _repository.Update(bestelling);
        }

        public Bestelling StatusNaarOpgehaald(long id)
        {
            var bestelling = _repository.Find(id);
            if (bestelling.Status != BestelStatus.Opgehaald)
            {
                bestelling.Status = BestelStatus.Opgehaald;
                var result = _repository.Update(bestelling);
                var statusUpdatedEvent = new BestellingStatusUpdatedEvent("can.bestellingbeheer.bestellingStatusUpdated")
                {
                    BestellingsNummer = bestelling.Bestellingnummer,
                    BestellingStatusCode = bestelling.Status.ToString()
                };
                _publisher.Publish(statusUpdatedEvent);
                return bestelling;
            }
            throw new InvalidBestelStatusException("Status staat al op opgehaald");
        }
        public void Dispose()
        {
            _repository?.Dispose();
            _publisher?.Dispose();
        }

        public Bestelling StatusNaarGoedgekeurd(long id)
        {
            var bestelling = _repository.Find(id);
            if (bestelling.Status != BestelStatus.Goedgekeurd)
            {
                bestelling.Status = BestelStatus.Goedgekeurd;
                _repository.Update(bestelling);
                BestellingStatusUpdatedEvent(bestelling);
                return bestelling;
            }
            throw new InvalidBestelStatusException("Status staat al op goedgekeurd");
        }

        public Bestelling StatusNaarAfgekeurd(long id)
        {
            var bestelling = _repository.Find(id);
            if (bestelling.Status != BestelStatus.Afgekeurd)
            {
                bestelling.Status = BestelStatus.Afgekeurd;
                _repository.Update(bestelling);
                BestellingStatusUpdatedEvent(bestelling);
                return bestelling;
            }
            throw new InvalidBestelStatusException("Status staat al op afgekeurd");
        }

        private void BestellingStatusUpdatedEvent(Bestelling bestelling)
        {
            var statusUpdatedEvent = new BestellingStatusUpdatedEvent("can.bestellingbeheer.bestellingStatusUpdated")
            {
                BestellingsNummer = bestelling.Bestellingnummer,
                BestellingStatusCode = bestelling.Status.ToString()
            };
            _publisher.Publish(statusUpdatedEvent);
        }
    }
}
