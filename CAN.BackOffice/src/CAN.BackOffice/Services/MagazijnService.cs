using CAN.BackOffice.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAN.BackOffice.Domain.Entities;
using CAN.BackOffice.Agents.BestellingsAgent.Agents;
using CAN.BackOffice.Mappers;
using CAN.BackOffice.Infrastructure.DAL.Repositories;
using CAN.BackOffice.Agents.BestellingsAgent.Agents.Models;
using Microsoft.Extensions.Logging;

namespace CAN.BackOffice.Services
{
    public class MagazijnService
        : IMagazijnService
    {
        private readonly ILogger<MagazijnService> _logger;
        private readonly IRepository<Bestelling, long> _repository;
        private readonly IBestellingBeheerService _service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="service"></param>
        public MagazijnService(IRepository<Bestelling, long> repo, 
            IBestellingBeheerService service,
            ILogger<MagazijnService> logger)
        {
            _repository = repo;
            _service = service;
            _logger = logger;
        }       

        /// <summary>
        /// Returns the next bestelling
        /// </summary>
        /// <returns></returns>
        public Bestelling GetVolgendeBestelling()
        {
            _logger.LogInformation("Volgende bestelling ophalen");
            return _repository.FindBy(a => a.BestellingStatusCode == "Goedgekeurd")
                .FirstOrDefault();
        }

        /// <summary>
        /// Updates the status of a bestelling
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void ZetBestellingOpOpgehaald(long id)
        {
            _logger.LogInformation("Bestelling op opgehaald laten zetten");
            var response = _service.BestellingStatusOpgehaald(id);
            if (response is string)
            {
                _logger.LogInformation("Response is een BestellingDTO");
                var bestelling = _repository.Find(id);
                bestelling.BestellingStatusCode = (response as string);
                _repository.Update(bestelling);
                _logger.LogInformation($"Bestelling {bestelling.Id} heeft status {bestelling.BestellingStatusCode} gekregen");
            }
            if (response is ErrorMessage)
            {
                var error = response as ErrorMessage;
                _logger.LogError($"response was een foutmelding: {error.FoutMelding}");
            }
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
