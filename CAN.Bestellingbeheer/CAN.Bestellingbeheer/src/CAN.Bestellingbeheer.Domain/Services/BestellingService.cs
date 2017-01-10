using CAN.Bestellingbeheer.Domain.Entities;
using CAN.Bestellingbeheer.Domain.Interfaces;
using InfoSupport.WSA.Infrastructure;
using System;

namespace CAN.Bestellingbeheer.Domain.Services {
    public class BestellingService : IDisposable
    {
        private readonly IRepository<Bestelling, long> _repository;

        public BestellingService(IEventPublisher publisher, IRepository<Bestelling, long> repository)
        {
            _repository = repository;
        }

        public int CreateBestelling(Bestelling bestelling)
        {
            return _repository.Insert(bestelling);
        }
        public int UpdateBestelling(Bestelling bestelling)
        {
            return _repository.Update(bestelling);
        }
        public void Dispose()
        {
            _repository?.Dispose();
        }


    }
}
