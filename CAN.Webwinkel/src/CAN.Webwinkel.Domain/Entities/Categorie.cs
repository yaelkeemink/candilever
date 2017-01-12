using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Domain.Entities
{
    public class Categorie
    {
        public long Id { get; set; }
        public string Naam { get; set; }
        public List<ArtikelCategorie> ArtikelCategorie { get; set; } = new List<ArtikelCategorie>();
    }
}
