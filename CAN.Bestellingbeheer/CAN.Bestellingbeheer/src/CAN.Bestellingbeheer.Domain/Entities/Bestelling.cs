using CAN.Bestellingbeheer.Domain.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAN.Bestellingbeheer.Domain.Entities
{
    public enum BestelStatus
    {
        Goedgekeurd = 0,
        Opgehaald = 100,
    }
    public class Bestelling
    {
        public long Bestellingnummer { get; set; }

        public IList<Artikel> Artikelen { get; set; }
        public DateTime BestelDatum { get; set; }
        public BestelStatus Status { get; set; }

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

        public Bestelling()
        {
            Artikelen = new List<Artikel>();
            BestelDatum = DateTime.Now; 
        }
        public Bestelling(BestellingDTO bestelling)
        {
            Klantnummer = bestelling.Klantnummer;
            Bestellingnummer = bestelling.Bestellingnummer;
            BestelDatum = bestelling.BestelDatum;
            Status = bestelling.Status;
            Artikelen = new List<Artikel>();
            foreach (var artikel in bestelling.Artikelen)
            {
                Artikelen.Add(artikel);
            }
        }
    }
}
