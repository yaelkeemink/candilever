using CAN.BackOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.BackOffice.Models
{
    public class FactuurViewModel
    {
        public string KlantNaam { get; set; }
        public string KlantAdres { get; set; }
        public string KlantHuisnummer { get; set; }
        public string KlantPostcode { get; set; }
        public string KlantWoonplaats { get; set; }
        public string KlantLand { get; set; }

        public Bestelling Bestelling { get; set; }
    }
}
