using CAN.Bestellingbeheer.Domain.Domain.Entities;
using CAN.Bestellingbeheer.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace CAN.Bestellingbeheer.Infrastructure.Infrastructure.Repositories
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
    }
}