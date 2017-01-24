using CAN.Webwinkel.Domain;
using CAN.Webwinkel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAN.Webwinkel.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CAN.Webwinkel.Domain.Services
{
    public class ArtikelService : IArtikelService
    {
        private readonly ILogger<ArtikelService> _logger;
        private readonly IRepository<Artikel, long> _repository;

        public ArtikelService(ILogger<ArtikelService> logger, IRepository<Artikel, long> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public IEnumerable<Artikel> ArtikelenBijCategorie(string categorieNaam)
        {
            return _repository.FindBy(
                    c => c.ArtikelCategorie.Any(ac => ac.Categorie.Naam == categorieNaam))
                .ToList();
        }

        public IEnumerable<Artikel> AlleArtikelen()
        {
            return _repository.FindAll();
        }

        public IEnumerable<Artikel> AlleArtikelenPerPagina(int paginanummer, int aantalArtikelen)
        {
            if (paginanummer < 0)
            {
                paginanummer = 0;
            }
            var paginas = AantalPaginas(aantalArtikelen);
            if (paginanummer > paginas)
            {
                paginanummer = paginas;
            }

            return _repository.FindAll().Skip((paginanummer - 1) * aantalArtikelen).Take(aantalArtikelen);
        }

        public int AantalPaginas(int aantalArtikelenPerPagina)
        {
            var aantalArtikelen = _repository.FindAll().Count();
            var aantalPaginas = aantalArtikelen / aantalArtikelenPerPagina;
            if (aantalArtikelen % aantalArtikelenPerPagina != 0)
            {
                aantalPaginas++;
            }
            return aantalPaginas;
        }
    }
}
