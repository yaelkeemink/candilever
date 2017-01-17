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
using CAN.Webwinkel.Domain.Services;
using CAN.Webwinkel.Models;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;

namespace CAN.Webwinkel.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ArtikelController : Controller
    {

        private readonly ILogger<ArtikelController> _logger;
        private readonly IArtikelService _service;

        public ArtikelController(ILogger<ArtikelController> logger, IArtikelService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [SwaggerOperation("AlleArtikelenBijCategorie")]
        [Route("{categorieNaam}")]
        public IActionResult Get(string categorieNaam)
        {
            _logger.LogInformation($"Alle artikelen opgevraagd bij categorie {categorieNaam}");
            var artikelen = _service.ArtikelenBijCategorie(categorieNaam);
            var lijst = artikelen.Select(a => new ApiArtikelenModel(a));
            return Json(lijst);
        }
    }
}