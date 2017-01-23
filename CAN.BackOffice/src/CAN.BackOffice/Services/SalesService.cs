using CAN.BackOffice.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAN.BackOffice.Domain.Entities;
using CAN.BackOffice.Agents.BestellingsAgent.Agents;
using CAN.BackOffice.Agents.BestellingsAgent.Agents.Models;

namespace CAN.BackOffice.Services
{
    public class SalesService
        : ISalesService
    {
        private readonly IRepository<Bestelling, long> _repo;
        private readonly IBestellingBeheerService _service;

        public SalesService(IRepository<Bestelling, long> repo, IBestellingBeheerService service)
        {
            _repo = repo;
            _service = service;
        }

        public void BestellingGoedkeuren(long id)
        {
            var response = _service.BestellingGoedkeuren(id);
            if(response is BestellingDTO)
            {
                var bestelling = _repo.Find(id);
                bestelling.BestellingStatusCode = (response as BestellingDTO).Status.ToString();
                _repo.Update(bestelling);
            }
        }        

        public IEnumerable<Bestelling> FindAllTeControleren()
        {
            return _repo.FindBy(a => a.BestellingStatusCode == "Goedgekeurd")                
                .ToList();
        }

        public void Dispose()
        {
            _repo?.Dispose();
        }

        public void BestellingAfkeuren(long id)
        {
            var response = _service.BestellingAfkeuren(id);
            if (response is BestellingDTO)
            {
                var bestelling = _repo.Find(id);
                bestelling.BestellingStatusCode = (response as BestellingDTO).Status.ToString();
                _repo.Update(bestelling);
            }
        }
    }
}
