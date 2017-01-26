using CAN.Webwinkel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.ViewModels.HomeViewModels
{
    public class WinkelmandjeViewModel
    {
        public Winkelmandje winkelmandje { get; set; }
        public decimal Totaalprijs { get; set; }

        public WinkelmandjeViewModel(Winkelmandje winkelmandje)
        {
            this.winkelmandje = winkelmandje;
            winkelmandje.Artikelen = winkelmandje.Artikelen.Select(a => new ArtikelDTO()
            {
                Id = a.Id,
                Prijs = Math.Round(a.Prijs * 1.21M * a.Aantal, 2),
                Aantal = a.Aantal,
                Artikelnummer = a.Artikelnummer,
                Leverancier = a.Leverancier,
                LeverancierCode = a.LeverancierCode,
                Naam = a.Naam,
            }).ToList();    
            Totaalprijs = winkelmandje.Artikelen.Sum(a => a.Prijs);
        }
    }
}
