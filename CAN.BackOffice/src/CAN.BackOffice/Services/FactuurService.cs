using CAN.BackOffice.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAN.BackOffice.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CAN.BackOffice.Services
{
    public class FactuurService : IFactuurService
    {
        private readonly IRepository<Bestelling, long> _repository;
        private readonly ILogger<FactuurService> _logger;

        public FactuurService(ILogger<FactuurService> logger, IRepository<Bestelling, long> repository)
        {
            _repository = repository;
            _logger = logger;
        }


        public Bestelling ZoekBestelling(int bestellingsnummer)
        {
            return _repository.Find(bestellingsnummer);
        }
    }
}
