using System;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;
using Microsoft.EntityFrameworkCore;
using CAN.WinkelmandjeBeheer.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using CAN.WinkelmandjeBeheer.Facade.Facade.Errors;
using CAN.WinkelmandjeBeheer.Domain.Domain.Entities;
using CAN.WinkelmandjeBeheer.Domain.Entities;

namespace CAN.WinkelmandjeBeheer.Facade.Facade.Controllers
{
    [Route("api/[controller]")]
    public class WinkelmandjeController : Controller
    {
        private readonly IWinkelmandjeService _service;
        private readonly ILogger<WinkelmandjeController> _logger;

        public WinkelmandjeController(IWinkelmandjeService service, ILogger<WinkelmandjeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // POST api/values
        [HttpPost]
        [SwaggerOperation("Post")]
        [ProducesResponseType(typeof(Winkelmandje), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateWinkelmandje([FromBody]Winkelmandje winkelmandje)
        {
            if (!ModelState.IsValid)
            {
                var error = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");
                return BadRequest(error);
            }
            try
            {
                var dbWinkelmandje = _service.CreateWinkelmandje(winkelmandje);
                _logger.LogInformation($"Winkelmandje met {dbWinkelmandje.WinkelmandjeNummer} is aangemaakt");
                return Ok(dbWinkelmandje);
            }
            catch (Exception ex)
            {
                var error = new ErrorMessage(ErrorTypes.Unknown,
                    $"Onbekende fout in create winkelmandje: {winkelmandje},/nException: {ex}");
                return BadRequest(error);
            }
        }

        // PUT api/values/5
        [HttpPut]
        [SwaggerOperation("Update")]
        [ProducesResponseType(typeof(Winkelmandje), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult ArtikelToevoegen([FromBody]Winkelmandje winkelmandje)
        {
            if (!ModelState.IsValid)
            {
                var error = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");
                return BadRequest(error);
            }
            try
            {
                var dbWinkelmandje = _service.UpdateWinkelmandje(winkelmandje);
                _logger.LogInformation($"{winkelmandje.Artikelen.Count} artikelen zijn toegevoegd aan het winkelmandje met nummer {dbWinkelmandje.WinkelmandjeNummer}");
                return Ok(dbWinkelmandje);
            }
            catch (DbUpdateException ex)
            {
                var error = new ErrorMessage(ErrorTypes.NotFound,
                        $"Fout met updaten in db: {winkelmandje}/nException: {ex}");
                return NotFound(error);
            }
            catch (Exception ex)
            {
                var error = new ErrorMessage(ErrorTypes.Unknown,
                        $"Onbekende fout bij updaten: {winkelmandje}/nException: {ex}");
                return BadRequest(error);
            }
        }

        [HttpPost]
        [SwaggerOperation("Afronden")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [Route("Finish")]
        public IActionResult WinkelmandjeAfronden([FromBody]Bestelling bestelling)
        {
            if (!ModelState.IsValid)
            {
                var error = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");
                return BadRequest(error);
            }
            try
            {
                _service.FinishWinkelmandje(bestelling);
                _logger.LogInformation($"Het winkelmandje met {bestelling.WinkelmandjeNummer} is verzonden naar de eventbus en de bestellingbeheernummer");
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                var error = new ErrorMessage(ErrorTypes.NotFound,
                        $"Fout met afronden in db: {bestelling}/nException: {ex}");
                return NotFound(error);
            }
            catch (Exception ex)
            {
                var error = new ErrorMessage(ErrorTypes.Unknown,
                        $"Onbekende fout bij afronden: {bestelling}/nException: {ex}");
                return BadRequest(error);
            }
        }
        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}
