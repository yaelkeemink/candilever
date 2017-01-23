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

                // Test, remove later
                Bestelling temp = new Bestelling()
                {
                    VolledigeNaam = "Lars Celie",
                    Adres = "Pythagoraslaan",
                    Huisnummer = "113E",
                    Woonplaats = "Utrecht",
                    Land = "Nederland",
                    Postcode = "3584BB",
                    BestelDatum = new DateTime(2017, 1, 23),
                    Bestellingsnummer = 1,
                    Klantnummer = 1,
                    Id = 1,
                    Artikelen = new List<Artikel>()
                    {
                        new Artikel() {
                            Artikelnummer = 1,
                            Artikelnaam = "Test artikel",
                            Aantal = 10,
                            Leverancier = "Unilever",
                            LeverancierCode = "187acak1",
                            Prijs = 600M,
                            Id = 1
                        }
                    }
                };

                return View(new FactuurViewModel(temp));
                //return RedirectToAction("FactuurNietGevonden");
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