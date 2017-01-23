using CAN.Common.Events;
using CAN.WinkelmandjeBeheer.Domain.Domain.Entities;
using CAN.WinkelmandjeBeheer.Domain.Domain.Interfaces;
using CAN.WinkelmandjeBeheer.Domain.DTO;
using CAN.WinkelmandjeBeheer.Domain.Entities;
using CAN.WinkelmandjeBeheer.Domain.Interfaces;
using System;
using System.Linq;
using InfoSupport.WSA.Infrastructure;
using Microsoft.Extensions.Logging;

namespace CAN.WinkelmandjeBeheer.Domain.Domain.Services
{
    public class WinkelmandjeService : IWinkelmandjeService, IDisposable
    {
        private readonly IRepository<Winkelmandje, Guid> _repository;
        private readonly IEventPublisher _publisher;
        private readonly ILogger<WinkelmandjeService> _logger;

        public WinkelmandjeService(IRepository<Winkelmandje, Guid> repository, IEventPublisher publisher, ILogger<WinkelmandjeService> logger)
        {
            _repository = repository;
            _publisher = publisher;
            _logger = logger;
        }

        public Guid CreateWinkelmandje(Winkelmandje winkelmandje)
        {
            _repository.Insert(winkelmandje);

            return winkelmandje.WinkelmandjeNummer;

        }
        public Guid UpdateWinkelmandje(Winkelmandje winkelmandje)
        {
            var dbWinkelmandje = _repository.Find(winkelmandje.WinkelmandjeNummer);

            foreach (var artikel in winkelmandje.Artikelen)
            {
                var dbArtikel = dbWinkelmandje.Artikelen.FirstOrDefault(db => db.Naam == artikel.Naam);

                if (dbArtikel != null)
                {
                    dbWinkelmandje.Artikelen.Add(artikel);
                }
                else
                {
                    dbArtikel.Aantal += 1;
                }
            }

            _repository.Update(dbWinkelmandje);

            return dbWinkelmandje.WinkelmandjeNummer;
        }

        public void FinishWinkelmandje(Bestelling bestelling)
        {
            var winkelmandje = _repository.Find(bestelling.WinkelmandjeNummer);
            var publishEvent = new WinkelmandjeAfgerondEvent("can.winkelmandjeservice.winkelmandjeafgerond")
            {
                Klantnummer = bestelling.Klantnummer,
                Adres = bestelling.Adres,
                Huisnummer = bestelling.Huisnummer,
                Land = bestelling.Land,
                Postcode = bestelling.Postcode,
                VolledigeNaam = bestelling.VolledigeNaam,
            };

            foreach (ArtikelDTO dto in winkelmandje.Artikelen)
            {
                publishEvent.AddArtikel(artikelNummer: dto.Artikelnummer, artikelNaam: dto.Naam, prijs: decimal.Parse(dto.Prijs), aantal: dto.Aantal, leverancierCode: dto.LeverancierCode, leverancier: dto.Leverancier);
            }

            _publisher.Publish(publishEvent);

        }
        public void Dispose()
        {
            _repository?.Dispose();
        }


    }
}
