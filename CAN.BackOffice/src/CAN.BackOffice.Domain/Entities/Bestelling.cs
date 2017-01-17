﻿using CAN.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Can.BackOffice.Domain.Entities
{
    public class Bestelling
    {

        public long Id { get; set; }

        public long Klantnummer { get; set; }

        public long Bestellingsnummer { get; set; }

        public DateTime BestelDatum { get; set; }

        public IList<Artikel> Artikelen { get; set; }

        public Bestelling() { }

        public Bestelling(BestellingCreatedEvent evt)
        {
            Artikelen = new List<Artikel>();

            Klantnummer = evt.Klantnummer;
            Bestellingsnummer = evt.Bestellingsnummer;
            BestelDatum = evt.BestelDatum;
            
            foreach(var artikel in evt.Artikelen)
            {
                Artikelen.Add(
                    new Artikel
                    {
                        Artikelnummer = artikel.Artikelnummer,
                        Artikelnaam = artikel.Artikelnaam,
                        Aantal = artikel.Aantal,
                        Prijs = artikel.Prijs
                    }
                );
            }
        }

    }
}