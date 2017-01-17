using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.BackOffice.Domain.Entities
{
    public enum BestelStatus
    {
        Goedgekeurd,
        opgehaald,
    }
    public class Bestelling
    {
        public long Bestellingnummer { get; set; }

        public long Klantnummer { get; set; }

        public IList<Artikel> Artikelen { get; set; }

        public DateTime BestelDatum { get; set; }
        public BestelStatus Status { get; set; }
    }
}
