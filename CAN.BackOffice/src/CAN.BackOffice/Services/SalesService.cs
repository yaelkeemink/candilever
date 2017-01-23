using CAN.BackOffice.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAN.BackOffice.Domain.Entities;

namespace CAN.BackOffice.Services
{
    public class SalesService
        : ISalesService
    {
        private readonly IRepository<Bestelling, long> _repo;

        public SalesService(IRepository<Bestelling, long> repo)
        {
            _repo = repo;
        }

        public void BestellingGoedkeuren(long id)
        {
            
        }

        public IEnumerable<Bestelling> FindAllTeControleren()
        {
            return _repo.FindBy(a => a.BestellingStatusCode == "Goedgekeurd")
                .OrderByDescending(a => a.BestelDatum)
                .ToList();
        }
    }
}
