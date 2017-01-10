using InfoSupport.WSA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Common.Events
{
    public class KlantCreatedEvent
        :DomainEvent
    {
        public KlantCreatedEvent(string routingKey) : base(routingKey)
        {
        }

        public long Klantnummer { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Tussenvoegsels { get; set; }
        public string Postcode { get; set; }
        public int Telefoonnummer { get; set; }
    }
}
