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
    public class FactuurController : Controller, IDisposable
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

                return View(new FactuurViewModel(bestelling));
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"Factuur met bestellingsnummer {bestellingsnummer} is niet gevonden", e);
                return RedirectToAction("FactuurNietGevonden");
            }
            catch (Exception e)
            {
                _logger.LogError($"Onbekende fout opgetreden bij factuur details met bestellingsnummer {bestellingsnummer}", e);
                return RedirectToAction("FactuurNietGevonden");
            }
        }

        // GET: Factuur/FactuurNietGevonden
        public ActionResult FactuurNietGevonden()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _service?.Dispose();
            base.Dispose(disposing);
        }
    }
}