using InfoSupport.WSA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Common.Events
{
    public class BestellingCreatedEvent : DomainEvent
    {
        public BestellingCreatedEvent(string routingKey) : base(routingKey)
        {
        }

        public long Bestellingsnummer { get; set; }

        public DateTime BestelDatum { get; set; }

    }
}
