using CAN.BackOffice.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAN.BackOffice.Domain.Entities;
using CAN.BackOffice.Agents.BestellingsAgent.Agents;

namespace CAN.BackOffice.Services
{
    public class MagazijnService
        : IMagazijnService
    {
        private IRepository<Bestelling, long> _Repo;
        private IBestellingBeheerService _service;

        public MagazijnService(IRepository<Bestelling, long> repo, IBestellingBeheerService service)
        {
            _Repo = repo;
            _service = service;
        }
        public Bestelling GetBestelling()
        {
            return _Repo.FindAll()
                .Where(a => a.Status == BestelStatus.Goedgekeurd)
                .OrderBy(a => a.BestelDatum)
                .FirstOrDefault();
        }

        public void UpdateBestelling(Bestelling bestelling)
        {
            var artikelen = new List<Agents.BestellingsAgent.Agents.Models.Artikel>();
            foreach(var artikel in bestelling.Artikelen)
            {
                artikelen.Add(new Agents.BestellingsAgent.Agents.Models.Artikel()
                {
                    Aantal = artikel.Aantal,
                    Artikelnummer = artikel.Artikelnummer,
                    Bestellingnummer = artikel.Bestellingnummer,
                    Naam = artikel.Naam,
                    Prijs = artikel.Prijs,
                });
            }

            var toUpdate = new Agents.BestellingsAgent.Agents.Models.Bestelling()
            {
                Artikelen = artikelen,
                BestelDatum = bestelling.BestelDatum,
                Bestellingnummer = bestelling.Bestellingnummer,
                Klantnummer = bestelling.Klantnummer,
            };
            _service.Update(toUpdate);
            _Repo.Update(bestelling);
        }
    }
}
