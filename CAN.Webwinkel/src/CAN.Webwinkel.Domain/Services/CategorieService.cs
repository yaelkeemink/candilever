using CAN.Webwinkel.Domain.Entities;
using CAN.Webwinkel.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Domain.Services
{
    public class CategorieService : ICategorieService
    {
        private readonly ILogger<CategorieService> _logger;
        private readonly IRepository<Categorie, int> _repository;

        public CategorieService(ILogger<CategorieService> logger, IRepository<Categorie, int> repository)
        {
            _logger = logger;
            _repository = repository;
        }


        public IEnumerable<string> AlleCategorieen()
        {
            return _repository.FindAll()
                .Select(c => c.Naam);
        }
    }
}
