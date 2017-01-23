using InfoSupport.WSA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Common.Events
{
    public class BestellingCreatedEvent : DomainEvent
    {
        public BestellingCreatedEvent(string routingKey) : base(routingKey)
        {
            Artikelen = new List<Artikel>();
        }

        public long Bestellingsnummer { get; set; }

        public DateTime BestelDatum { get; set; }

        public int BestellingStatusNummer { get; set; }
        public string BestellingStatusCode { get; set; }

        public IList<Artikel> Artikelen { get; internal set; }
        
        public long Klantnummer { get; set; }
        public string VolledigeNaam { get; set; }
        public string Postcode { get; set; }
        public string Adres { get; set; }
        public string Huisnummer { get; set; }
        public string Land { get; set; }

        public void AddArtikel(long artikelNummer, string artikelNaam, decimal prijs, int aantal, string leverancierCode, string leverancier)
        {
            Artikelen.Add(new Artikel(artikelNummer, artikelNaam, prijs, aantal, leverancierCode, leverancier));
        }

        public class Artikel
        {
            internal Artikel()
            {

            }

            internal Artikel(long artikelNummer, string artikelNaam, decimal prijs, int aantal, string leverancierCode, string leverancier)
            {
                Artikelnummer = artikelNummer;
                Artikelnaam = artikelNaam;
                Prijs = prijs;
                Aantal = aantal;
                Leverancier = leverancier;
                LeverancierCode = leverancierCode;
            }

            public long Artikelnummer { get; set; }

            public string Artikelnaam { get; set; }

            public decimal Prijs { get; set; }

            public int Aantal { get; set; }
            public string Leverancier { get; set; }
            public string LeverancierCode { get; set; }
        }
    }
}
