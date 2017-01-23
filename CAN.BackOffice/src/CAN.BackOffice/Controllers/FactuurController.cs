using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CAN.BackOffice.Models;
using Microsoft.Extensions.Logging;
using CAN.BackOffice.Domain.Interfaces;
using CAN.BackOffice.Domain.Entities;

namespace CAN.BackOffice.Controllers
{
    public class FactuurController : Controller
    {
        private readonly ILogger<FactuurController> _logger;
        private readonly IFactuurService _service;

        public FactuurController(ILogger<FactuurController> logger, IFactuurService service)
        {
            _logger = logger;
            _service = service;
        }

        // GET: Factuur/Details/5
        public ActionResult Details(int bestellingsnummer)
        {
            _logger.LogInformation($"Factuur met bestellingsnummer {bestellingsnummer} wordt opgezocht");
            try
            {
                Bestelling bestelling = _service.ZoekBestelling(bestellingsnummer);
                _logger.LogInformation($"Factuur met bestellingsnummer {bestellingsnummer} is gevonden", bestelling);

                return View(new FactuurViewModel()
                {
                    KlantNaam = "Lars Celie",
                    KlantAdres = "Pythagoraslaan",
                    KlantHuisnummer = "113E",
                    KlantLand = "Nederland",
                    KlantPostcode = "3584BB",
                    KlantWoonplaats = "Utrecht",
                    Bestelling = new Bestelling()
                });
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"Factuur met bestellingsnummer {bestellingsnummer} is niet gevonden", e);
                return RedirectToAction("FactuurNietGevonden");
            }
        }

        // GET: Factuur/FactuurNietGevonden
        public ActionResult FactuurNietGevonden()
        {
            return View();
        }
    }
}