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
        private readonly IRepository<Categorie, int> _repository;

        public ArtikelService(ILogger<ArtikelService> logger, IRepository<Categorie, int> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public IEnumerable<Artikel> ArtikelenBijCategorie(string categorieNaam)
        {
            var query =  _repository.FindAll()
                 .Select(c => c.ArtikelCategorie.Select(ac => ac.Artikel))
                 .ToList();
            return null;
        }
    }
}
