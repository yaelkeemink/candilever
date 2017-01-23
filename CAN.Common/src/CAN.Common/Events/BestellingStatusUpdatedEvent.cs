using InfoSupport.WSA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Common.Events
{
    public class BestellingStatusUpdatedEvent
        :DomainEvent
    {
        public BestellingStatusUpdatedEvent(string routingKey) : base(routingKey)
        {
        }

        public long BestellingsNummer { get; set; }
        public string BestellingStatusCode { get; set; }
    }
}
