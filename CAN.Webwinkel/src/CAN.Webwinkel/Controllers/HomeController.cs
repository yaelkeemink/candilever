using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CAN.Webwinkel.Domain.Interfaces;
using CAN.Webwinkel.Models;
using CAN.Webwinkel.Domain.Entities;
using CAN.Webwinkel.Models.HomeViewModels;

namespace CAN.Webwinkel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<ArtikelController> _logger;
        private readonly IArtikelService _artikelService;
        private readonly IWinkelwagenService _winkelmandjeservice;

        public HomeController(ILogger<ArtikelController> logger, IArtikelService artikelService, IWinkelwagenService winkelmandjeService)
        {
            _logger = logger;
            _artikelService = artikelService;
            _winkelmandjeservice = winkelmandjeService;
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
            var artikelen = _artikelService.AlleArtikelenPerPagina(id, aantalArtikelenPerPagina)
                .Select(a => new ApiArtikelenModel(a))
                .ToList();
            int paginas = _artikelService.AantalPaginas(aantalArtikelenPerPagina);

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
        
        public IActionResult ToonWinkelmandje(string id)
        {
            WinkelmandjeViewModel viewModel = new WinkelmandjeViewModel(_winkelmandjeservice.FindWinkelmandje(id));
            return View(viewModel);
        }
    }
}
