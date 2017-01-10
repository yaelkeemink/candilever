using CAN.Klantbeheer.Domain.Domain.Entities;
using CAN.Klantbeheer.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace CAN.Klantbeheer.Infrastructure.Infrastructure.Repositories
{
    public class KlantRepository
        : BaseRepository<Klant, long, DatabaseContext>
    {
        public KlantRepository(DatabaseContext context) 
            : base(context)
        {
        }

        protected override DbSet<Klant> GetDbSet()
        {
            return _context.Klanten;
        }

        protected override long GetKeyFrom(Klant item)
        {
            return item.Klantnummer;
        }
    }
}