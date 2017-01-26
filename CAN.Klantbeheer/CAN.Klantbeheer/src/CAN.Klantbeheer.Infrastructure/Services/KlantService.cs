using CAN.Common.Events;
using CAN.Klantbeheer.Domain.Entities;
using CAN.Klantbeheer.Domain.Interfaces;
using InfoSupport.WSA.Infrastructure;
using Microsoft.Extensions.Logging;
using System;

namespace CAN.Klantbeheer.Infrastructure.Services
{
    public class KlantService 
        : IDisposable, IKlantService
    {
        private readonly IRepository<Klant, long> _repository;
        private readonly IEventPublisher _publisher;
        private readonly ILogger<KlantService> _logger;

        public KlantService(IRepository<Klant, long> repository, 
            IEventPublisher publisher,
            ILogger<KlantService> logger)
        {
            _repository = repository;
            _publisher = publisher;
            _logger = logger;
        }

        public long CreateKlant(Klant klant)
        {
            long toReturn = 0;
            _logger.LogInformation("Controlleren of er een telefoonnummer en/of email ingevuld is");
            if (!string.IsNullOrEmpty(klant.Telefoonnummer) || !string.IsNullOrEmpty(klant.Email))
            {
                _logger.LogInformation("telefoonnummer en/of email is ingevuld");
                _repository.Insert(klant);

                toReturn = klant.Klantnummer;
                _publisher.Publish(new KlantCreatedEvent("can.klantbeheer.klantcreated")
                {
                    Klantnummer = klant.Klantnummer,
                    Voornaam = klant.Voornaam,
                    Tussenvoegsels = klant.Tussenvoegsels,
                    Achternaam = klant.Achternaam,
                    Postcode = klant.Postcode,
                    Telefoonnummer = klant.Telefoonnummer,
                    Adres = klant.Straatnaam,
                    Email = klant.Email,
                    Huisnummer = klant.Huisnummer,
                    Land = klant.Land.ToString(),
                });
            }
            _logger.LogWarning("Geen email en telefoonnummer ingevuld");
            return toReturn;
        }
        public int UpdateKlant(Klant klant)
        {
            _logger.LogInformation("Klant upodaten");
            return _repository.Update(klant);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
