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
using Microsoft.AspNetCore.Authorization;

namespace CAN.BackOffice.Controllers
{
    public class FactuurController : BaseController, IDisposable
    {
        private readonly IFactuurService _service;

        public FactuurController(ILogger<FactuurController> logger, IFactuurService service)
            : base (logger)
        {
            _service = service;
        }
        [Authorize(Roles ="Sales")]
        // GET: Factuur/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Bestelling bestelling = _service.ZoekBestelling(id);
                _logger.LogInformation($"Factuur met bestellingsnummer {id} is gevonden", bestelling);

                return View(new FactuurViewModel(bestelling));
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"Factuur met bestellingsnummer {id} is niet gevonden", e);
                return RedirectToAction("FactuurNietGevonden");
            }
            catch (Exception e)
            {
                _logger.LogError($"Onbekende fout opgetreden bij factuur details met bestellingsnummer {id}", e);
                return RedirectToAction("Error");
            }
        }

        [Authorize(Roles = "Sales")]
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