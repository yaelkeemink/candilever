using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CAN.BackOffice.Domain.Interfaces;
using CAN.BackOffice.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CAN.BackOffice.Controllers
{
    public class SalesController 
        : BaseController
    {
        private readonly ISalesService _service;

        public SalesController(ILogger logger,
            ISalesService service) 
            : base(logger)
        {
            _service = service;
        }

        public IActionResult Index()
        {            
            IEnumerable<Bestelling> viewModel = _service.FindAllTeControleren();
            _logger.LogInformation("Index pagina terugsturen");
            return View(viewModel);
        }

        public IActionResult Goedkeuren(long id)
        {
            _service.BestellingGoedkeuren(id);
            _logger.LogInformation("Redirect naar Index");
            return RedirectToAction("Index");
        }

        public IActionResult Afkeuren(long id)
        {
            _service.BestellingAfkeuren(id);
            _logger.LogInformation("Redirect naar Index");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _service?.Dispose();
            base.Dispose(disposing);
        }
    }
}