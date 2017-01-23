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
        private readonly IRepository<Bestelling, long> _repo;
        private readonly IBestellingBeheerService _service;
        private readonly ILogger _logger;

        public SalesService(IRepository<Bestelling, long> repo, 
            IBestellingBeheerService service,
            ILogger logger)
        {
            _repo = repo;
            _service = service;
            _logger = logger;
        }

        public void BestellingGoedkeuren(long id)
        {            
            var response = _service.BestellingGoedkeuren(id);
            if(response is BestellingDTO)
            {                
                var bestelling = _repo.Find(id);
                bestelling.BestellingStatusCode = (response as BestellingDTO).Status.ToString();
                _repo.Update(bestelling);
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
            return _repo.FindBy(a => a.BestellingStatusCode == "Geplaatst")                
                .ToList();
        }       

        public void BestellingAfkeuren(long id)
        {
            var response = _service.BestellingAfkeuren(id);
            if (response is BestellingDTO)
            {
                var bestelling = _repo.Find(id);
                bestelling.BestellingStatusCode = (response as BestellingDTO).Status.ToString();
                _repo.Update(bestelling);
                _logger.LogInformation($"Bestelling geupdate met status: {bestelling.BestellingStatusCode}");
            }
            if (response is ErrorMessage)
            {
                var error = response as ErrorMessage;
                _logger.LogError($"response was een foutmelding: {error.FoutMelding}");
            }
            _logger.LogError($"Onbekende response: {response}");
        }

        public void Dispose()
        {
            _repo?.Dispose();
        }
    }
}
