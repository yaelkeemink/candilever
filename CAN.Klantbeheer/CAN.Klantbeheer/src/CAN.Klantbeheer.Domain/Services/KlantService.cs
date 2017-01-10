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

        public int CreateKlant(Klant player)
        {
            return _repository.Insert(player);
        }
        public int UpdateKlant(Klant player)
        {
            return _repository.Update(player);
        }
        public void Dispose()
        {
            _repository?.Dispose();
        }


    }
}
