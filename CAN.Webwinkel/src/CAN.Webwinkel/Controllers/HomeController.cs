using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CAN.Webwinkel.Domain.Interfaces;
using CAN.Webwinkel.Models;
using CAN.Webwinkel.Domain.Entities;

namespace CAN.Webwinkel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<ArtikelController> _logger;
        private readonly IArtikelService _service;

        public HomeController(ILogger<ArtikelController> logger, IArtikelService service)
        {
            _logger = logger;
            _service = service;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Index", new { id = 1 });
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Index(int id)
        {
            var aantalArtikelenPerPagina = 24;
            var artikelen = _service.AlleArtikelenPerPagina(id, aantalArtikelenPerPagina)
                .Select(a => new ApiArtikelenModel(a))
                .ToList();
            int paginas = _service.AantalPaginas(aantalArtikelenPerPagina);

            return View(new ArtikelOverzichtModel() { Artikelen = artikelen, AantalPaginas = paginas, HuidigePaginanummer = id } );
        }

        [HttpGet]
        public IActionResult Registreren()
        {
            return View();
        }
        public IActionResult AlgemeneVoorwaarden()
        {
            return View();
        }

        public IActionResult WettelijkeTeksten()
        {
            return View();
        }
    }
}
