using CAN.Common.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public Bestelling(WinkelmandjeAfgerondEvent evt)
        {
            Artikelen = new List<Artikel>();
            BestelDatum = DateTime.Now;

            Klantnummer = evt.Klantnummer;
            VolledigeNaam = evt.VolledigeNaam;
            Postcode = evt.Postcode;
            Adres = evt.Adres;
            Huisnummer = evt.Huisnummer;
            Land = evt.Land;

            foreach (var artikel in evt.Artikelen)
            {
                Artikelen.Add(
                    new Artikel
                    {
                        Artikelnummer = artikel.Artikelnummer,
                        Naam = artikel.Artikelnaam,
                        Prijs = artikel.Prijs,
                        Aantal = artikel.Aantal,
                        Leverancier = artikel.Leverancier,
                        LeverancierCode = artikel.LeverancierCode
                    }
                );
            }

            AutomatischGoedkeurenTotaalBedragBinnenLimiet();
        }
        
        private void AutomatischGoedkeurenTotaalBedragBinnenLimiet()
        {
            if (500 <= Artikelen.Sum(n => n.Prijs))
                Status = BestelStatus.Goedgekeurd;
        }
    }
}
