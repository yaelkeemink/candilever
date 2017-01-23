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
        [Required]
        public string VolledigeNaam { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string Adres { get; set; }
        [Required]
        public string Huisnummer { get; set; }
        [Required]
        public string Land { get; set; }

        public IList<ArtikelDTO> Artikelen { get; set; }
        public DateTime BestelDatum { get; set; }
        public BestelStatus Status { get; set; }
        public BestellingDTO()
        {
            Artikelen = new List<ArtikelDTO>();
            BestelDatum = DateTime.Now;
        }

        public BestellingDTO(Bestelling bestelling)
        {
            Klantnummer = bestelling.Klantnummer;
            Bestellingnummer = bestelling.Bestellingnummer;
            BestelDatum = bestelling.BestelDatum;
            Status = bestelling.Status;
            VolledigeNaam = bestelling.VolledigeNaam;
            Postcode = bestelling.Postcode;
            Adres = bestelling.Adres;
            Huisnummer = bestelling.Huisnummer;
            Land = bestelling.Land;
        Artikelen = new List<ArtikelDTO>();
            foreach (var artikel in bestelling.Artikelen)
            {
                Artikelen.Add(artikel);
            }
        }
    }
}
