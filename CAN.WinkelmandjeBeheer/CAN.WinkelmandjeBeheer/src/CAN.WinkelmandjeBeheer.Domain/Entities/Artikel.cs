using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.WinkelmandjeBeheer.Domain.Entities
{
    public class Artikel
    {
        public Artikel()
        {

        }

        public Artikel(long artikelNummer, string artikelNaam, decimal prijs, int aantal, string leverancierCode, string leverancier)
        {
            Artikelnummer = artikelNummer;
            Artikelnaam = artikelNaam;
            Prijs = prijs;
            Aantal = aantal;
            Leverancier = leverancier;
            LeverancierCode = leverancierCode;
        }

        public long Id { get; set; }
        public long Artikelnummer { get; set; }

        public string Artikelnaam { get; set; }

        public decimal Prijs { get; set; }

        public int Aantal { get; set; }
        public string Leverancier { get; set; }
        public string LeverancierCode { get; set; }
    }
}
