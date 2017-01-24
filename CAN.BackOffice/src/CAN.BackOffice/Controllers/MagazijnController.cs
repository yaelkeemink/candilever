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
    public class MagazijnController : BaseController
    {
        private IMagazijnService _service;

        public MagazijnController(ILogger<MagazijnController> logger,
            IMagazijnService service) 
            : base(logger)
        {
            _service = service;
        }

        public IActionResult GeenBestelling()
        {
            return View();
        }

        /// <summary>
        /// Haal volgende bestelling op
        /// </summary>
        /// <returns></returns>
        public IActionResult BestellingOphalen()
        {
            try
            {
                var viewModel = _service.GetVolgendeBestelling();
                if (viewModel == null)
                {
                    return RedirectToAction("GeenBestelling");
                }
                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.LogError($"Er is iets fout gegaan: {e}");
                return RedirectToAction("Error");
            }
        }


        /// <summary>
        /// Zet huidige bestlling op opgehaald en returnd de volgende bestelling
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public IActionResult VolgendeBestellingOphalen(int id)
        {
            try
            {
                _service.ZetBestellingOpOpgehaald(id);
                return RedirectToAction("BestellingOphalen");
            }
            catch (Exception e)
            {
                _logger.LogError($"Er is iets fout gegaan: {e}");
                return RedirectToAction("Error");
            }
        }

        protected override void Dispose(bool disposing)
        {
            _service?.Dispose();
            base.Dispose(disposing);
        }
    }
}