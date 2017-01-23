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
        public IEnumerable<Bestelling> FindAllTeControlleren()
        {
            return _repo.FindAll()
                .Where(a => a.BestellingStatusCode == "Goedgekeurd")
                .OrderByDescending(a => a.BestelDatum)
                .ToList();
        }
    }
}
