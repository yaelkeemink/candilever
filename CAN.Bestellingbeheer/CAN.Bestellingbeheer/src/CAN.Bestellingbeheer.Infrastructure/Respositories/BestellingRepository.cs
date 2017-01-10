using CAN.Bestellingbeheer.Domain.Entities;
using CAN.Bestellingbeheer.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CAN.Bestellingbeheer.Infrastructure.Repositories
{
    public class BestellingRepository
        : BaseRepository<Bestelling, long, DatabaseContext>
    {
        public BestellingRepository(DatabaseContext context) 
            : base(context)
        {
        }

        protected override DbSet<Bestelling> GetDbSet()
        {
            return _context.Bestellingen;
        }

        protected override long GetKeyFrom(Bestelling item)
        {
            return item.Id;
        }

        public override IEnumerable<Bestelling> FindAll()
        {
            return GetDbSet()
                .Include(n => n.Artikelen);
        }
        
        public override Bestelling Find(long id)
        {
            return GetDbSet().
                Include(n => n.Artikelen)
                .Single(a => GetKeyFrom(a).Equals(id));
        }
    }
}