using CAN.BackOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.BackOffice.Models.SalesViewModels
{
    public class SalesDetailsViewModel
    {
        public long Id { get; set; }
        public long Bestellingsnummer { get; set; }

        public string VolledigeNaam { get; set; }
        public string Telefoonnummer { get; set; }
        public string Email { get; set; }

        public decimal TotaalBedrag { get; set; }
        public IEnumerable<Artikel> Artikelen { get; set; }

        public SalesDetailsViewModel(Klant klant, Bestelling bestelling)// <-- Hier mapping of in de controller?
        {
            Id = bestelling.Id;
            Bestellingsnummer = bestelling.Bestellingsnummer;
            VolledigeNaam = $"{klant.Voornaam} {klant.Tussenvoegsel} {klant.Achternaam}";
            Telefoonnummer = klant.Telefoonnummer;
            Email = klant.Email;
            Artikelen = bestelling.Artikelen;

            TotaalBedrag = 0;
            foreach(var artikel in Artikelen)
            {
                TotaalBedrag += artikel.Prijs * artikel.Aantal;
            }
        }
    }
}
