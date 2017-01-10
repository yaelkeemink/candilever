using CAN.Klantbeheer.Domain.Domain.Entities;
using CAN.Klantbeheer.Domain.Domain.Interfaces;
using System;

namespace CAN.Klantbeheer.Domain.Domain.Services {
    public class KlantService : IDisposable
    {
        private readonly IRepository<Klant, long> _repository;

        public KlantService(IRepository<Klant, long> repository)
        {
            _repository = repository;
        }

        public int CreatePlayer(Klant player)
        {
            return _repository.Insert(player);
        }
        public int UpdatePlayer(Klant player)
        {
            return _repository.Update(player);
        }
        public void Dispose()
        {
            _repository?.Dispose();
        }


    }
}
