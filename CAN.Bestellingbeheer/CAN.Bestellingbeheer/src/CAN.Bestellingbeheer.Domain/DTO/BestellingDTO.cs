using CAN.Bestellingbeheer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Bestellingbeheer.Domain.DTO
{
    public class BestellingDTO
    {
        public long Bestellingnummer { get; set; }

        [Required]
        public long Klantnummer { get; set; }

        public IList<ArtikelDTO> Artikelen { get; set; }
        public DateTime BestelDatum { get; set; }
        public BestelStatus Status { get; set; }

        public BestellingDTO(Bestelling bestelling)
        {
            Klantnummer = bestelling.Klantnummer;
            Bestellingnummer = bestelling.Bestellingnummer;
            BestelDatum = bestelling.BestelDatum;
            Status = bestelling.Status;
            Artikelen = new List<ArtikelDTO>();
            foreach (var artikel in bestelling.Artikelen)
            {
                Artikelen.Add(artikel);
            }
        }
    }
}
