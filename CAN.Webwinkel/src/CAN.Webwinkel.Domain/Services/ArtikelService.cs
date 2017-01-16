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
        private readonly IRepository<Artikel, int> _repository;

        public ArtikelService(ILogger<ArtikelService> logger, IRepository<Artikel, int> repository)
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
    }
}
