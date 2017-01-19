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
        public IActionResult BestellingOphalen()
        {
            var viewModel = _service.GetVolgendeBestelling();
            return View(viewModel);
        }
        [HttpPut]
        public IActionResult BestellingOphalen(long id)
        {
            _service.ZetBestellingOpOpgehaald(id);
            var viewModel = _service.GetVolgendeBestelling();
            return View(viewModel);
        }
    }
}