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
        private readonly IBestellingBeheerService _agent;
        private readonly ILogger<SalesService> _logger;
        private readonly IRepository<Klant, long> _klantRepository;

        public SalesService(IRepository<Bestelling, long> bestellingRepository,
            IRepository<Klant, long> klantRepository, 
            IBestellingBeheerService service,
            ILogger<SalesService> logger)
        {
            _bestellingRepository = bestellingRepository;
            _klantRepository = klantRepository;
            _agent = service;
            _logger = logger;
        }

        public void BestellingGoedkeuren(long bestellingsnummer)
        {            
            var response = _agent.BestellingGoedkeuren(bestellingsnummer);
            if(response is string)
            {                
                var bestelling = _bestellingRepository.FindBy(b => b.Bestellingsnummer == bestellingsnummer).Single();
                bestelling.BestellingStatusCode = (response as string);
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

        public void BestellingAfkeuren(long bestellingsnummer)
        {
            var response = _agent.BestellingAfkeuren(bestellingsnummer);
            if (response is string)
            {
                var bestelling = _bestellingRepository.FindBy(b => b.Bestellingsnummer == bestellingsnummer).Single();
                bestelling.BestellingStatusCode = (response as string);
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

        public Bestelling FindBestelling(long bestellingsnummer)
        {
            _logger.LogInformation("Zoek alle bestellingen");
            return _bestellingRepository.FindBy(b => b.Bestellingsnummer == bestellingsnummer).Single();
        }

        public void Dispose()
        {
            _bestellingRepository?.Dispose();
        }
    }
}
