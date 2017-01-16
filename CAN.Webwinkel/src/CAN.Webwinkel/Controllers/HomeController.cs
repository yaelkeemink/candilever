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
            var artikelen = _service.AlleArtikelen();
            var lijst = artikelen.Select(a => new ApiArtikelenModel(a))
                .ToList();

            return View(lijst);
        }

        [HttpGet]
        public IActionResult Registreren()
        {
            return View();
        }
    }
}
