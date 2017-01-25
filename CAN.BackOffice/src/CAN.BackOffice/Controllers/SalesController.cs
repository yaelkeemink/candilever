using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CAN.BackOffice.Domain.Interfaces;
using CAN.BackOffice.Domain.Entities;
using Microsoft.Extensions.Logging;
using CAN.BackOffice.Models.SalesViewModels;
using Microsoft.AspNetCore.Authorization;

namespace CAN.BackOffice.Controllers
{
    public class SalesController
        : BaseController
    {
        private readonly ISalesService _service;

        public SalesController(ILogger<SalesController> logger,
            ISalesService service)
            : base(logger)
        {
            _service = service;
        }
        [Authorize(Roles = "Sales")]
        public IActionResult Index()
        {
            try
            {
                IEnumerable<Bestelling> bestellingen = _service.FindAllTeControleren();
                List<SalesIndexViewModel> viewModel = new List<SalesIndexViewModel>();
                foreach (var bestelling in bestellingen)
                {
                    viewModel.Add(bestelling);
                }

                _logger.LogInformation("Index pagina terugsturen");
                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.LogError($"Er is iets fout gegaan: {e}");
                return RedirectToAction("Error");
            }
        }
        [Authorize(Roles = "Sales")]
        public IActionResult Goedkeuren(long id)
        {
            try
            {
                _service.BestellingGoedkeuren(id);
                _logger.LogInformation("Redirect naar Index");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError($"Er is iets fout gegaan: {e}");
                return RedirectToAction("Error");
            }
        }
        [Authorize(Roles = "Sales")]
        public IActionResult Afkeuren(long id)
        {
            try
            {
                _service.BestellingAfkeuren(id);
                _logger.LogInformation("Redirect naar Index");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError($"Er is iets fout gegaan: {e}");
                return RedirectToAction("Error");
            }
        }
        [Authorize(Roles = "Sales")]
        public IActionResult Details(long id)
        {
            var bestelling = _service.FindBestelling(id);
            var klant = _service.FindKlant(bestelling.Klantnummer);
            var viewModel = new SalesDetailsViewModel(klant, bestelling);
            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            _service?.Dispose();
            base.Dispose(disposing);
        }
    }
}