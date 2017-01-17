using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.BackOffice.Domain.Entities
{
    public class Artikel
    {
        public long Id { get; set; }
        public long Artikelnummer { get; set; }

        public string Artikelnaam { get; set; }

        public decimal Prijs { get; set; }

        public int Aantal { get; set; }
    }
}
