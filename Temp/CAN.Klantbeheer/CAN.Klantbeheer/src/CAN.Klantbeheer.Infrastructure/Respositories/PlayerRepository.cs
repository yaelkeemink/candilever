using Microsoft.EntityFrameworkCore;

namespace CAN.Klantbeheer.Infrastructure.Infrastructure.Repositories
{
    public class PlayerRepository
        : BaseRepository<Player, long, DatabaseContext>
    {
        public PlayerRepository(DatabaseContext context) 
            : base(context)
        {
        }

        protected override DbSet<Player> GetDbSet()
        {
            return _context.Players;
        }

        protected override long GetKeyFrom(Player item)
        {
            return item.Id;
        }
    }
}