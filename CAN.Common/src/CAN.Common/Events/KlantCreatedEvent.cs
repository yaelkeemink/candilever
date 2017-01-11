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
        public string Telefoonnummer { get; set; }
        public string Adres { get; set; }
        public string Email { get; set; }
        public string Huisnummer { get; set; }
        public string Land { get; set; }
    }
}
