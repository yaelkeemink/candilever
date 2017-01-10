using CAN.Klantbeheer.Domain.Entities;
using CAN.Klantbeheer.Infrastructure.DAL;
using CAN.Klantbeheer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CAN.Klantbeheer.Infrastructure.Repositories
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