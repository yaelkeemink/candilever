using CAN.BackOffice.Domain.Entities;
using System.Linq;

namespace CAN.BackOffice.Infrastructure.DAL.Repositories
{
    public class KlantRepository : BaseRepository<Klant, long, DatabaseContext>
    {
        public KlantRepository(DatabaseContext context) : base(context)
        {
        }

        protected override IQueryable<Klant> GetDbSet()
        {
            return _context.Klanten;
        }

        protected override long GetKeyFrom(Klant item)
        {
            return item.Klantnummer;
        }
    }
}
