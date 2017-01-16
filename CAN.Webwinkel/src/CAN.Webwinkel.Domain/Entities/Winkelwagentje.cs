using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Domain.Entities
{
    public class Winkelwagentje
    {
        public Winkelwagentje()
        {
            Artikelen = new List<BestelArtikel>();
        }
        public long Bestellingnummer { get; set; }

        public long Klantnummer { get; set; }

        public IList<BestelArtikel> Artikelen { get; set; }

        public DateTime BestelDatum { get; set; }
    }
    public class BestelArtikel
    {
        public long Bestellingnummer { get; set; }

        public long Artikelnummer { get; set; }

        public string Naam { get; set; }

        public double Prijs { get; set; }

        public int Aantal { get; set; }
    }
}
