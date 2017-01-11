using CAN.Webwinkel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Models
{
    public class ApiArtikelenModel
    {
        
        public int Artikelnummer { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public decimal Prijs { get; set; }
        public string AfbeeldingUrl { get; set; }

        public ApiArtikelenModel()
        {

        }

        public ApiArtikelenModel(Artikel a)
        {
            Artikelnummer = a.Artikelnummer;
            Naam = a.Naam;
            Beschrijving = a.Beschrijving;
            Prijs = a.Prijs;
            AfbeeldingUrl = a.AfbeeldingUrl;
        }
    }
}
