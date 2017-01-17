using CAN.BackOffice.Agents.BestellingsAgent.Agents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.BackOffice.Mappers
{
    public class BestellingMapper
    {
        public static Bestelling Map(Domain.Entities.Bestelling besteling)
        {
            var artikelen = new List<Artikel>();
            foreach(var artikel in besteling.Artikelen)
            {
                artikelen.Add(MapArtikel(artikel));
            }
            return new Bestelling()
            {
                Artikelen = artikelen,
                BestelDatum = besteling.BestelDatum,
                Bestellingnummer = besteling.Bestellingnummer,
                Klantnummer = besteling.Klantnummer,
            };
        }

        private static Artikel MapArtikel(Domain.Entities.Artikel artikel)
        {
            return new Artikel()
            {
                Aantal = artikel.Aantal,
                Artikelnummer = artikel.Artikelnummer,
                Bestellingnummer = artikel.Bestellingnummer,
                Naam = artikel.Naam,
                Prijs = artikel.Prijs,
            };
        }
    }
}
