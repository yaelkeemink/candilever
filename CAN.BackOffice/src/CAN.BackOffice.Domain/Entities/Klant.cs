using CAN.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.BackOffice.Domain.Entities
{
    public class Klant
    {
        public long Id { get; set; }
        public long Klantnummer { get; set; }
        public string Telefoonnummer { get; set; }
        public string Email { get; set; }

        public Klant() { }

        public Klant(KlantCreatedEvent evt) {

            Klantnummer = evt.Klantnummer;
            Telefoonnummer = evt.Telefoonnummer;
            Email = evt.Email;
        }

    }
}
