using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CAN.Webwinkel.Domain.Entities;

namespace CAN.Webwinkel.Infrastructure.DAL.Repositories
{
    public class ArtikelRepository : BaseRepository<Artikel, int, WinkelDatabaseContext>
    {
        public ArtikelRepository(WinkelDatabaseContext context) : base(context)
        {
        }

        protected override IQueryable<Artikel> GetDbSet()
        {
            return _context.Artikels;
        }

        protected override int GetKeyFrom(Artikel item)
        {
            return item.Artikelnummer;
        }
    }
}
