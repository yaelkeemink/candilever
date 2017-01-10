using CAN.Klantbeheer.Domain.Domain.Entities;
using CAN.Klantbeheer.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace CAN.Klantbeheer.Infrastructure.Infrastructure.Repositories
{
    public class PlayerRepository
        : BaseRepository<Klant, long, DatabaseContext>
    {
        public PlayerRepository(DatabaseContext context) 
            : base(context)
        {
        }

        protected override DbSet<Klant> GetDbSet()
        {
            return _context.Players;
        }

        protected override long GetKeyFrom(Klant item)
        {
            return item.Klantnummer;
        }
    }
}