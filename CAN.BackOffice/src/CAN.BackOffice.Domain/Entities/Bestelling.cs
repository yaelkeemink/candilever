﻿using CAN.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CAN.BackOffice.Domain.Entities
{

    public class Bestelling
    {       
        public long Id { get; set; }

        public long Klantnummer { get; set; }
        public string VolledigeNaam { get; set; }
        public string Adres { get; set; }
        public string Huisnummer { get; set; }
        public string Postcode { get; set; }
        public string Land { get; set; }


        public long Bestellingsnummer { get; set; }
        public DateTime BestelDatum { get; set; }

        public IList<Artikel> Artikelen { get; set; }
        public int BestellingStatusNumber { get; set; } // <-- depricated?
        public string BestellingStatusCode { get; set; }
        public string Woonplaats { get; set; }

        public Bestelling()
        {
            Artikelen = new List<Artikel>();
        }
        public Bestelling(BestellingCreatedEvent evt)
        {
            Artikelen = new List<Artikel>();

            Klantnummer = evt.Klantnummer;
            Bestellingsnummer = evt.Bestellingsnummer;
            BestelDatum = evt.BestelDatum;
            BestellingStatusNumber = evt.BestellingStatusNummer;
            BestellingStatusCode = evt.BestellingStatusCode;
            VolledigeNaam = evt.VolledigeNaam;
            Adres = evt.Adres;
            Huisnummer = evt.Huisnummer;
            Postcode = evt.Postcode;
            Land = evt.Land;

            foreach (var artikel in evt.Artikelen)
            {
                Artikelen.Add(
                    new Artikel
                    {
                        Artikelnummer = artikel.Artikelnummer,
                        Artikelnaam = artikel.Artikelnaam,
                        Aantal = artikel.Aantal,
                        Prijs = artikel.Prijs,
                        Leverancier = artikel.Leverancier,
                        LeverancierCode = artikel.LeverancierCode,
                    }
                );
            }
        }

    }
}
