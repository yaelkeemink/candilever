using CAN.Bestellingbeheer.Domain.Entities;
using System.Collections.Generic;

namespace CAN.Bestellingbeheer.Domain.Domain.Entities
{
    public class Bestelling
    {
        public long Id { get; set; }

        public IList<Artikel> Artikelen { get; set; }
    }
}
