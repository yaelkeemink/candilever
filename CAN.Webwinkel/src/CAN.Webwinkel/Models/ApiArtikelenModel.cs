using CAN.Webwinkel.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Models
{
    public class ApiArtikelenModel
    {
        
        public long Artikelnummer { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public decimal Prijs { get; set; }
        public string AfbeeldingUrl { get; set; }
        public int Voorraad { get; set; }
        public string Leverancier { get; set; }
        public string LeverancierCode { get; set; }

        public string GetJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        public ApiArtikelenModel()
        {

        }

        public ApiArtikelenModel(Artikel artikel)
        {
            Artikelnummer = artikel.Artikelnummer;
            Naam = artikel.Naam;
            Beschrijving = artikel.Beschrijving;
            Prijs = artikel.Prijs;
            AfbeeldingUrl = "images/"+artikel.AfbeeldingUrl;
            Voorraad = artikel.Voorraad >= 8 ? 8 : artikel.Voorraad;
            Leverancier = artikel.Leverancier;
            LeverancierCode = artikel.LeverancierCode;
        }
    }
}
