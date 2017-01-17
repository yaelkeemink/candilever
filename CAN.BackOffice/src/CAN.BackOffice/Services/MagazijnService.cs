using CAN.BackOffice.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAN.BackOffice.Domain.Entities;
using CAN.BackOffice.Agents.BestellingsAgent.Agents;
using CAN.BackOffice.Mappers;

namespace CAN.BackOffice.Services
{
    public class MagazijnService
        : IMagazijnService
    {
        private IRepository<Bestelling, long> _Repo;
        private IBestellingBeheerService _service;

        public MagazijnService(IRepository<Bestelling, long> repo, IBestellingBeheerService service)
        {
            _Repo = repo;
            _service = service;
        }
        public Bestelling GetBestelling()
        {
            return _Repo.FindBy(a => a.Status == BestelStatus.Goedgekeurd)
                .OrderBy(a => a.BestelDatum)
                .FirstOrDefault();
        }

        public int UpdateStatusBestelling(long id)
        {
            _service.Update(id);
            return _Repo.Update(bestelling);
        }
    }
}
