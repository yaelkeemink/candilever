using CAN.Common.Events;
using CAN.Klantbeheer.Domain.Entities;
using CAN.Klantbeheer.Domain.Interfaces;
using InfoSupport.WSA.Infrastructure;
using System;

namespace CAN.Klantbeheer.Domain.Services {
    public class KlantService : IDisposable
    {
        private readonly IRepository<Klant, long> _repository;
        private readonly IEventPublisher _publisher;

        public KlantService(IRepository<Klant, long> repository, IEventPublisher publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        public int CreateKlant(Klant klant)
        {
            int toReturn = _repository.Insert(klant);
            //TODO: routingkey
            _publisher.Publish(new KlantCreatedEvent("can.klantbeheer.klantcreated")
            {
                Klantnummer = klant.Klantnummer,
                Voornaam = klant.Voornaam,
                Tussenvoegsels = klant.Tussenvoegsels,
                Achternaam = klant.Achternaam,
                Postcode = klant.Postcode,
                Telefoonnummer = klant.Telefoonnummer,
                Adres = klant.Adres,
                Email = klant.Email,
                Huisnummer = klant.Huisnummer,
                Land = klant.Land,
            });
            
            return toReturn;
        }
        public int UpdateKlant(Klant klant)
        {
            return _repository.Update(klant);
        }
        public void Dispose()
        {
            _repository?.Dispose();
        }


    }
}
