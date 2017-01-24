using CAN.Webwinkel.Domain.Entities;
using CAN.Webwinkel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Domain.Services
{
    public class WinkelwagenService : IWinkelwagenService
    {
        private readonly IRepository<Winkelmandje, string> _repository;

        public WinkelwagenService(IRepository<Winkelmandje, string> repository)
        {
            _repository = repository;
        }
        public void Insert(Winkelmandje mandje)
        {
            _repository.Insert(mandje);
        }

        public void Update(Winkelmandje mandje)
        {
            var dbWinkelMandje = _repository.Find(mandje.WinkelmandjeNummer);

            dbWinkelMandje = mandje;

            _repository.Update(dbWinkelMandje);
        }
    }
}
