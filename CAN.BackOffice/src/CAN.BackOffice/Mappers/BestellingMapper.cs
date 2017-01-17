using CAN.BackOffice.Agents.BestellingsAgent.Agents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.BackOffice.Mappers
{
    public class BestellingMapper
    {
        public static BestellingDTO Map(Domain.Entities.Bestelling besteling)
        {
            var artikelen = new List<ArtikelDTO>();
            foreach(var artikel in besteling.Artikelen)
            {
                artikelen.Add(MapArtikel(artikel));
            }
            return new BestellingDTO()
            {
                Artikelen = artikelen,
                BestelDatum = besteling.BestelDatum,
                Bestellingnummer = besteling.Bestellingsnummer,
                Klantnummer = besteling.Klantnummer,
            };
        }

        private static ArtikelDTO MapArtikel(Domain.Entities.Artikel artikel)
        {
            return new ArtikelDTO()
            {
                Id = artikel.Id,
                Aantal = artikel.Aantal,
                Artikelnummer = artikel.Artikelnummer,
                Naam = artikel.Artikelnaam,
                Prijs = artikel.Prijs.ToString(),
            };
        }
    }
}
