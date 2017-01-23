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
        private readonly ISalesService _salesService;

        public SalesController(ISalesService service)
        {
            _salesService = service;
        }
        public IActionResult Index()
        {
            IEnumerable<Bestelling> viewModel = _salesService.FindAllTeControleren();
            return View(viewModel);
        }
    }
}