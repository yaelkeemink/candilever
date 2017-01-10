using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Bestellingbeheer.Domain.Entities
{
    public class Artikel
    {
        public long Id { get; set; }

        public decimal Prijs { get; set; }
    }
}
