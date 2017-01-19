using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CAN.BackOffice.Domain.Entities;

namespace CAN.BackOffice.Infrastructure.DAL.Repositories
{
    public class BestellingRepository 
        : BaseRepository<Bestelling, long, DatabaseContext>
    {
        public BestellingRepository(DatabaseContext context) : base(context)
        {
        }

        protected override IQueryable<Bestelling> GetDbSet()
        {
            return _context.Bestellingen.Include(n => n.Artikelen);
        }

        protected override long GetKeyFrom(Bestelling item)
        {
            return item.Bestellingsnummer;
        }


        public override Bestelling FindVolgendeBestelling()
        {
            return GetDbSet().Where(a => a.BestellingStatusCode == "Goedgekeurd")
                      .OrderBy(a => a.BestelDatum)
                      .FirstOrDefault();
        }
    }
}
