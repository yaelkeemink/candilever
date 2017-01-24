using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Domain.Entities
{
    public class ArtikelDTO
    {
        public long Id { get; set; }

        public long Artikelnummer { get; set; }

        [Required]
        public string Naam { get; set; }

        [Required]
        public decimal Prijs { get; set; }

        [Required]
        public int Aantal { get; set; }

        public string Leverancier { get; set; }
        public string LeverancierCode { get; set; }

        public ArtikelDTO()
        {

        }
    }
}