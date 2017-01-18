using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CAN.BackOffice.Domain.Interfaces;
using CAN.BackOffice.Domain.Entities;

namespace CAN.BackOffice.Controllers
{
    public class MagazijnController : Controller
    {
        private IMagazijnService _service;
        public MagazijnController(IMagazijnService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Bestelling()
        {
            var viewModel = _service.GetBestelling();
            return View(viewModel);
        }
        [HttpPut]
        public IActionResult Bestelling(long id)
        {
            _service.ZetBestellingOpOpgehaald(id);
            var viewModel = _service.GetBestelling();
            return View(viewModel);
        }
    }
}