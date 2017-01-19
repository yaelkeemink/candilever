using CAN.BackOffice.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAN.BackOffice.Domain.Entities;
using CAN.BackOffice.Agents.BestellingsAgent.Agents;
using CAN.BackOffice.Mappers;
using CAN.BackOffice.Infrastructure.DAL.Repositories;

namespace CAN.BackOffice.Services
{
    public class MagazijnService
        : IMagazijnService
    {
        private BestellingRepository _Repo;
        private IBestellingBeheerService _service;

        public MagazijnService(BestellingRepository repo, IBestellingBeheerService service)
        {
            _Repo = repo;
            _service = service;
        }
        public Bestelling GetVolgendeBestelling()
        {
            return _Repo.FindVolgendeBestelling();
        }

        public int ZetBestellingOpOpgehaald(long id)
        {
            var response = _service.BestellingStatusOpgehaald(id);
            if (response.GetType() == typeof(int))
            {
                return (int)response;
            }
            return 0;
        }
    }
}
