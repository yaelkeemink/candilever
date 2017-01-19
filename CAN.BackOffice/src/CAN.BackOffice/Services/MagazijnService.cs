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
        private IRepository<Bestelling, long> _Repo;
        private IBestellingBeheerService _service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="service"></param>
        public MagazijnService(IRepository<Bestelling, long> repo, IBestellingBeheerService service)
        {
            _Repo = repo;
            _service = service;
        }
        /// <summary>
        /// Returns the next bestelling
        /// </summary>
        /// <returns></returns>
        public Bestelling GetVolgendeBestelling()
        {
            return _Repo.FindVolgendeBestelling();
        }

        /// <summary>
        /// Updates the status of a bestelling
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
