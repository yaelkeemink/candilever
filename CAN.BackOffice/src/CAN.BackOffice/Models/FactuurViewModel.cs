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


    }
}
