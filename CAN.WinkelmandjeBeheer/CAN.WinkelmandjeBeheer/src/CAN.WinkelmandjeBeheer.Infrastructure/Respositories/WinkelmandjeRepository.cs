using CAN.WinkelmandjeBeheer.Domain.Domain.Entities;
using CAN.WinkelmandjeBeheer.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAN.WinkelmandjeBeheer.Infrastructure.Infrastructure.Repositories
{
    public class WinkelmandjeRepository
        : BaseRepository<Winkelmandje, string, DatabaseContext>
    {
        public WinkelmandjeRepository(DatabaseContext context) 
            : base(context)
        {
        }

        protected override DbSet<Winkelmandje> GetDbSet()
        {
            return _context.Winkelmandjes;
        }

        protected override string GetKeyFrom(Winkelmandje item)
        {
            return item.WinkelmandjeNummer;
        }

        public override Winkelmandje Find(string id)
        {
            return _context.Winkelmandjes.Include(x => x.Artikelen).FirstOrDefault(w => w.WinkelmandjeNummer == id);
        }
    }
}