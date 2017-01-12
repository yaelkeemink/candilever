using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAN.Bestellingbeheer.Domain.Entities
{
    public class Bestelling
    {
        public long Bestellingnummer { get; set; }

        [Required]
        public long Klantnummer { get; set; }

        public IList<Artikel> Artikelen { get; set; }
        public DateTime BestelDatum { get; set; }

        public Bestelling()
        {
            Artikelen = new List<Artikel>();
            BestelDatum = DateTime.Now; 
        }
    }
}
