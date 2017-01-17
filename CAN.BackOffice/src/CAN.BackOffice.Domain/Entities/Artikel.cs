using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.BackOffice.Domain.Entities
{
    public class Artikel
    {
        public long Bestellingnummer { get; set; }
        
        public Bestelling Bestelling { get; set; }
        
        public long Artikelnummer { get; set; }
        
        public string Naam { get; set; }
        
        public double Prijs { get; set; }
        
        public int Aantal { get; set; }
    }
}
