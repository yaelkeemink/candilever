using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CAN.Webwinkel.Infrastructure.DAL.Repositories;
using CAN.Webwinkel.Domain.Entities;
using CAN.Webwinkel.Domain.Interfaces;

namespace CAN.Webwinkel.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ArtikelController : Controller
    {

        private readonly ILogger<ArtikelController> _logger;
        private readonly IRepository<Categorie, int> _repository;

        public ArtikelController(ILogger<ArtikelController> logger, IRepository<Categorie, int> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [Route("{categorieNaam}")]
        public IEnumerable<Artikel> Get(string categorieNaam)
        {
            var query = _repository.FindAll()
                .Select(c => c.ArtikelCategorie.Select(ac => ac.Artikel));
            return null;
        }
    }
}