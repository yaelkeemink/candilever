using System;
using System.Collections.Generic;

namespace CAN.Bestellingbeheer.Domain.Entities
{
    public class Bestelling
    {
        public long Id { get; set; }

        public IList<Artikel> Artikelen { get; set; }
        public DateTime BestelDatum { get; set; } = DateTime.Now;

        public Bestelling()
        {
            Artikelen = new List<Artikel>();
        }
    }
}
