using CAN.Bestellingbeheer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Bestellingbeheer.Domain.DTO
{
    public class ArtikelDTO
    {
        public long Id { get; set; }

        public long Artikelnummer { get; set; }

        [Required]
        public string Naam { get; set; }

        [Required]
        public string Prijs { get; set; }

        [Required]
        public int Aantal { get; set; }

        public static implicit operator Artikel(ArtikelDTO artikel)
        {
            return new Artikel()
                {
                    Aantal = artikel.Aantal,
                    Artikelnummer = artikel.Artikelnummer,
                    Id = artikel.Id,
                    Naam = artikel.Naam,
                    Prijs = decimal.Parse(artikel.Prijs),
                };
        }

    }
}
