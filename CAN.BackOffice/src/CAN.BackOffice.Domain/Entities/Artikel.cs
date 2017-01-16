using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Can.BackOffice.Domain.Entities
{
    public class Artikel
    {
        public Bestelling Bestelling { get; set; }

        public long Artikelnummer { get; set; }

        public string Artikelnaam { get; set; }

        public decimal Prijs { get; set; }

        public int Aantal { get; set; }
    }
}
