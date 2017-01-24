using CAN.Webwinkel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace CAN.Webwinkel.Infrastructure.DAL.Repositories
{
    public class WinkelmandjeRepository
        : BaseRepository<Winkelmandje, long, WinkelDatabaseContext>
    {
        public WinkelmandjeRepository(WinkelDatabaseContext context) 
            : base(context)
        {
        }

        protected override IQueryable<Winkelmandje> GetDbSet()
        {
            return _context.Winkelmandjes;
        }

        protected override long GetKeyFrom(Winkelmandje item)
        {
            return item.Id;
        }

        public override IEnumerable<Winkelmandje> FindBy(Expression<Func<Winkelmandje, bool>> filter)
        {
            return GetDbSet()
                .Where(filter);
        }
    }
}
