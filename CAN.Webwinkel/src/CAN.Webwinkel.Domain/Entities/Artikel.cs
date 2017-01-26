using CAN.Webwinkel.Domain.Entities;
using Kantilever.Catalogusbeheer.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Domain.Entities
{
    public class Artikel
    {
        public long Id { get; set; }
        public int Artikelnummer { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public decimal Prijs { get; set; }
        public string AfbeeldingUrl { get; set; }
        public DateTime LeverbaarVanaf { get; set; }
        public DateTime? LeverbaarTot { get; set; }
        public string LeverancierCode { get; set; }
        public string Leverancier { get; set; }
        public int Voorraad { get; set; }


        public Artikel()
        {
        }

        public Artikel(ArtikelAanCatalogusToegevoegd evt)
        {
            Artikelnummer = evt.Artikelnummer;
            Naam = evt.Naam;
            Beschrijving = evt.Beschrijving;
            Prijs = evt.Prijs;
            AfbeeldingUrl = evt.AfbeeldingUrl;
            LeverbaarVanaf = evt.LeverbaarVanaf;
            LeverbaarTot = evt.LeverbaarTot;
            LeverancierCode = evt.LeverancierCode;
            Leverancier = evt.Leverancier;            
        }
    }
}
