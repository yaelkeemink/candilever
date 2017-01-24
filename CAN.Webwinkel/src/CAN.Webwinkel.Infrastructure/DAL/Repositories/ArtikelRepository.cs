using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CAN.Webwinkel.Domain.Entities;

namespace CAN.Webwinkel.Infrastructure.DAL.Repositories
{
    public class ArtikelRepository : BaseRepository<Artikel, long, WinkelDatabaseContext>
    {
        public ArtikelRepository(WinkelDatabaseContext context) : base(context)
        {
        }

        protected override IQueryable<Artikel> GetDbSet()
        {
            return _context.Artikels.Include(ar => ar.ArtikelCategorie)
                .ThenInclude(ac => ac.Categorie);
        }

        protected override long GetKeyFrom(Artikel item)
        {
            return item.Artikelnummer;
        }

        public override int Insert(Artikel item)
        {
            var artikelCategories = item.ArtikelCategorie;

            foreach(var ac in artikelCategories)
            {
                var catNam = ac.Categorie.Naam;
                Categorie realCat =_context.Categorieen.FirstOrDefault(c => c.Naam == catNam);
                if(realCat != null)
                {
                    ac.Categorie = realCat;
                }
            }


            return base.Insert(item);
        }
    }
}
