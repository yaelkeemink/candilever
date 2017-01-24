using CAN.BackOffice.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAN.BackOffice.Domain.Entities;
using CAN.BackOffice.Agents.BestellingsAgent.Agents;
using CAN.BackOffice.Agents.BestellingsAgent.Agents.Models;
using Microsoft.Extensions.Logging;

namespace CAN.BackOffice.Services
{
    public class SalesService
        : ISalesService
    {
        private readonly IRepository<Bestelling, long> _bestellingRepository;
        private readonly IBestellingBeheerService _service;
        private readonly ILogger<SalesService> _logger;
        private readonly IRepository<Klant, long> _klantRepository;

        public SalesService(IRepository<Bestelling, long> bestellingRepository,
            IRepository<Klant, long> klantRepository, 
            IBestellingBeheerService service,
            ILogger<SalesService> logger)
        {
            _bestellingRepository = bestellingRepository;
            _klantRepository = klantRepository;
            _service = service;
            _logger = logger;
        }

        public void BestellingGoedkeuren(long id)
        {            
            var response = _service.BestellingGoedkeuren(id);
            if(response is BestellingDTO)
            {                
                var bestelling = _bestellingRepository.Find(id);
                bestelling.BestellingStatusCode = (response as BestellingDTO).Status.ToString();
                _bestellingRepository.Update(bestelling);
                _logger.LogInformation($"Bestelling geupdate met status: {bestelling.BestellingStatusCode}");
            }
            if (response is ErrorMessage)
            {
                var error = response as ErrorMessage;
                _logger.LogError($"response was een foutmelding: {error.FoutMelding}");
            }
            _logger.LogError($"Onbekende response: {response}");
        }        

        public IEnumerable<Bestelling> FindAllTeControleren()
        {
            _logger.LogInformation("Alle geplaatste bestellingen");
            return _bestellingRepository.FindBy(a => a.BestellingStatusCode == "Geplaatst")                
                .ToList();
        }       

        public void BestellingAfkeuren(long id)
        {
            var response = _service.BestellingAfkeuren(id);
            if (response is BestellingDTO)
            {
                var bestelling = _bestellingRepository.Find(id);
                bestelling.BestellingStatusCode = (response as BestellingDTO).Status.ToString();
                _bestellingRepository.Update(bestelling);
                _logger.LogInformation($"Bestelling geupdate met status: {bestelling.BestellingStatusCode}");
            }
            if (response is ErrorMessage)
            {
                var error = response as ErrorMessage;
                _logger.LogError($"response was een foutmelding: {error.FoutMelding}");
            }
            _logger.LogError($"Onbekende response: {response}");
        }        

        public Klant FindKlant(long klantnummer)
        {
            _logger.LogInformation("Zoek klant op klantnummer");
            return _klantRepository.FindBy(a => a.Klantnummer == klantnummer)
                .Single();
        }

        public Bestelling FindBestelling(long id)
        {
            _logger.LogInformation("Zoek alle bestellingen");
            return _bestellingRepository.Find(id);
        }

        public void Dispose()
        {
            _bestellingRepository?.Dispose();
        }
    }
}
