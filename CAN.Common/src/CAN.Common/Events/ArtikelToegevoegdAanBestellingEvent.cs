using InfoSupport.WSA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Common.Events
{
    public class ArtikelToegevoegdAanBestellingEvent : DomainEvent
    {
        public ArtikelToegevoegdAanBestellingEvent(string routingKey) : base(routingKey)
        {
        }

        public long Bestellingsnummer { get; set; }

        public long Artikelnummer { get; set; }

        public string Artikelnaam { get; set; }

        public decimal Prijs { get; set; }

        public int Aantal { get; set; }


    }
}
