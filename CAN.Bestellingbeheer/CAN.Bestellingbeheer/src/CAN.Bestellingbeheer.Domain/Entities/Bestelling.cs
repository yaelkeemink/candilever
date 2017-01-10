using System.Collections.Generic;

namespace CAN.Bestellingbeheer.Domain.Entities
{
    public class Bestelling
    {
        public long Id { get; set; }

        public IList<Artikel> Artikelen { get; set; }

        public Bestelling()
        {
            Artikelen = new List<Artikel>();
        }
    }
}
