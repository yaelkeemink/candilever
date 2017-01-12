using CAN.Webwinkel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Infrastructure.DAL.Repositories
{
    public class CategorieRepository : BaseRepository<Categorie, int, WinkelDatabaseContext>
    {
        public CategorieRepository(WinkelDatabaseContext context) : base(context)
        {
        }

        protected override IQueryable<Categorie> GetDbSet()
        {
            return _context.Categorieen;
        }

        protected override int GetKeyFrom(Categorie item)
        {
            throw new NotImplementedException();
        } 
    }
}
