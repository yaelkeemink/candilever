using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Domain.Entities
{
    public class ArtikelCategorie
    {
        public long ArtikelId { get; set; }
        public Artikel Artikel { get; set; }

        public long CategoryId { get; set; }
        public Categorie Categorie { get; set; }
    }
}
