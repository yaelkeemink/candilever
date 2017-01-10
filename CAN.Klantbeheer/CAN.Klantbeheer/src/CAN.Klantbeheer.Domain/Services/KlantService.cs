using CAN.Klantbeheer.Domain.Entities;
using CAN.Klantbeheer.Domain.Interfaces;
using System;

namespace CAN.Klantbeheer.Domain.Services {
    public class KlantService : IDisposable
    {
        private readonly IRepository<Klant, long> _repository;

        public KlantService(IRepository<Klant, long> repository)
        {
            _repository = repository;
        }

        public int CreateKlant(Klant klant)
        {
            return _repository.Insert(klant);
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
