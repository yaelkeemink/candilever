using CAN.Webwinkel.Infrastructure.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CAN.Webwinkel.Infrastructure.DAL.Repositories
{
    public class ArtikelRepository : BaseRepository<Artikel, int, WinkelDatabaseContext>
    {
        public ArtikelRepository(WinkelDatabaseContext context) : base(context)
        {
        }

        protected override IQueryable<Artikel> GetDbSet()
        {
            return _context.Artikels.Include(ar => ar.ArtikelCategory);
        }

        protected override int GetKeyFrom(Artikel item)
        {
            return item.Artikelnummer;
        }

        public override int Insert(Artikel item)
        {
            var artikelCategories = item.ArtikelCategory;

            foreach(var ac in artikelCategories)
            {
                var catNam = ac.Category.Naam;
                Category realCat =_context.Categorieen.FirstOrDefault(c => c.Naam == catNam);
                if(realCat != null)
                {
                    ac.Category = realCat;
                }
            }


            return base.Insert(item);
        }
    }
}
