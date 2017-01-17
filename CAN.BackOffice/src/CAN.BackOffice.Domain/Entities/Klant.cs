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
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Tussenvoegsels { get; set; }
        public string Postcode { get; set; }
        public string Telefoonnummer { get; set; }
        public string Adres { get; set; }
        public string Email { get; set; }
        public string Huisnummer { get; set; }
        public string Land { get; set; }

        public Klant() { }

        public Klant(KlantCreatedEvent evt) {

            Klantnummer = evt.Klantnummer;
            Voornaam = evt.Voornaam;
            Achternaam = evt.Achternaam;
            Tussenvoegsels = evt.Tussenvoegsels;
            Postcode = evt.Postcode;
            Telefoonnummer = evt.Telefoonnummer;
            Adres = evt.Adres;
            Email = evt.Email;
            Huisnummer = evt.Huisnummer;
            Land = evt.Land;
        }

    }
}
