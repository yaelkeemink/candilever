using System;
using System.ComponentModel.DataAnnotations;

namespace CAN.Bestellingbeheer.Domain.Entities
{
    public class Artikel
    {
        public long Bestellingnummer { get; set; }

        public Bestelling Bestelling { get; set; }
        
        public long Artikelnummer { get; set; }

        [Required]
        public string Naam { get; set; }

        [Required]
        public decimal Prijs { get; set; }

        [Required]
        public int Aantal { get; set; }
    }
}
