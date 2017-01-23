using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CAN.BackOffice.Domain.Interfaces;
using CAN.BackOffice.Domain.Entities;

namespace CAN.BackOffice.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISalesService _service;

        public SalesController(ISalesService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            IEnumerable<Bestelling> viewModel = _service.FindAllTeControleren();
            return View(viewModel);
        }

        public IActionResult Goedkeuren(long id)
        {
            _service.BestellingGoedkeuren(id);
            return RedirectToAction("Index");
        }
    }
}