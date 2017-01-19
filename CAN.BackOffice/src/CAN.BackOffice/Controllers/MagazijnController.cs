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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public MagazijnController(IMagazijnService service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Haal volgende bestelling op
        /// </summary>
        /// <returns></returns>
        public IActionResult BestellingOphalen()
        {
            var viewModel = _service.GetVolgendeBestelling();
            return View(viewModel);
        }


        /// <summary>
        /// Zet huidige bestlling op opgehaald en returnd de volgende bestelling
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult BestellingOphalen(long id)
        {
            _service.ZetBestellingOpOpgehaald(id);
            var viewModel = _service.GetVolgendeBestelling();
            return View(viewModel);
        }
    }
}