﻿using CAN.Webwinkel.Domain.Entities;
using CAN.Webwinkel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CAN.Webwinkel.Infrastructure.Services
{
    public class WinkelmandjeService : IWinkelwagenService
    {
        private readonly IRepository<Winkelmandje, string> _repository;

        public WinkelmandjeService(IRepository<Winkelmandje, string> repository)
        {
            _repository = repository;
        }

        public Winkelmandje FindWinkelmandje(string guid)
        {
            return _repository.FindBy(a => a.WinkelmandjeNummer == guid)                
                .Single();
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
