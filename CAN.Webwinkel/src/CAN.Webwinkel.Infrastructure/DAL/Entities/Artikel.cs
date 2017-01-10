using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Infrastructure.DAL.Entities
{
    public class Artikel
    {
        public long Id { get; set; }
        public int Artikelnummer { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public decimal Prijs { get; set; }

        public string AfbeeldingUrl { get; set; }
        public DateTime LeverbaarVanaf { get; set; }
        public DateTime? LeverbaarTot { get; set; }
        public string LeverancierCode { get; set; }
        public string Leverancier { get; set; }

        public List<ArtikelCategory> ArtikelCategory { get; set; } = new List<ArtikelCategory>();



    }
}
