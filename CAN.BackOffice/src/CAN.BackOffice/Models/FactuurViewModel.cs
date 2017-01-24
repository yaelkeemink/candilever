using CAN.BackOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.BackOffice.Models
{
    public class FactuurViewModel
    {
        public Bestelling Bestelling { get; set; }

        public FactuurViewModel(Bestelling bestelling)
        {
            Bestelling = bestelling;
        }

        public decimal Totaalprijs
        {
            get { return Math.Round(Bestelling.Artikelen.Sum(a => a.Prijs), 2); }
            set { Totaalprijs = value; }
        }

        public decimal TotaalPrijsInclusiefBtw
        {
            get { return Math.Round(Totaalprijs * 1.21M, 2); }
        }
    }
}
