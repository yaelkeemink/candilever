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
        
        public decimal TotaalPrijsExclusiefBtw { get; set; }

        public decimal TotaalPrijsInclusiefBtw
        {
            get { return Math.Round(TotaalPrijsExclusiefBtw * 1.21M, 2); }
        }

        public FactuurViewModel(Bestelling bestelling)
        {
            Bestelling = bestelling;
            TotaalPrijsExclusiefBtw = bestelling.Artikelen.Sum(a => a.Prijs);
            PrijsInclusief();
        }

        private void PrijsInclusief()
        {
            foreach(var artikel in Bestelling.Artikelen)
            {
                artikel.Prijs = Math.Round(artikel.Prijs * 1.21M, 2);
            }
        }
    }
}
