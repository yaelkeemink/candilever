using CAN.WinkelmandjeBeheer.Domain.Domain.Entities;
using CAN.WinkelmandjeBeheer.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using System;

namespace CAN.WinkelmandjeBeheer.Infrastructure.Infrastructure.Repositories
{
    public class WinkelmandjeRepository
        : BaseRepository<Winkelmandje, Guid, DatabaseContext>
    {
        public WinkelmandjeRepository(DatabaseContext context) 
            : base(context)
        {
        }

        protected override DbSet<Winkelmandje> GetDbSet()
        {
            return _context.Winkelmandjes;
        }

        protected override Guid GetKeyFrom(Winkelmandje item)
        {
            return item.WinkelmandjeNummer;
        }
    }
}