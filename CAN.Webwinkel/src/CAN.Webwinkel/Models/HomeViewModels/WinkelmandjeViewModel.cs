using CAN.Webwinkel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Models.HomeViewModels
{
    public class WinkelmandjeViewModel
    {
        public Winkelmandje winkelmandje { get; set; }
        public decimal Totaalprijs { get; set; }

        public WinkelmandjeViewModel(Winkelmandje winkelmandje)
        {
            this.winkelmandje = winkelmandje;
            Totaalprijs = winkelmandje.Artikelen.Sum(a => a.Prijs);
        }
    }
}
