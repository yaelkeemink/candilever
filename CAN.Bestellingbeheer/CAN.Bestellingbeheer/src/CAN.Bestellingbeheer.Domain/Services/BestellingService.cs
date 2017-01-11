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

        public int CreateBestelling(Bestelling bestelling)
        {
            try
            {
                long bestellingsnummer = _repository.Insert(bestelling);
                _publisher.Publish(new BestellingCreatedEvent("l")
                {
                    Bestellingsnummer = bestellingsnummer,
                    BestelDatum = bestelling.BestelDatum,
                }
                );

                _logger.LogInformation("Bestelling created.", bestelling);

            } catch(Exception e)
            {
                _logger.LogError("Bestelling created failed.", bestelling, e.Message);
            }

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
