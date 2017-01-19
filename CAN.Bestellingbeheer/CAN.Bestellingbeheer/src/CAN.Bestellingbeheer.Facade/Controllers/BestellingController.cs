using System;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;
using Microsoft.EntityFrameworkCore;
using CAN.Bestellingbeheer.Domain.Services;
using CAN.Bestellingbeheer.Domain.Entities;
using CAN.Bestellingbeheer.Facade.Errors;
using Microsoft.Extensions.Logging;
using CAN.Bestellingbeheer.Domain.Interfaces;
using CAN.Bestellingbeheer.Domain.DTO;

namespace CAN.Bestellingbeheer.Facade.Controllers
{
    [Route("api/[controller]")]
    public class BestellingController : Controller
    {
        private readonly IBestellingService _service;
        private readonly ILogger<IBestellingService> _logger;

        public BestellingController(IBestellingService service, ILogger<IBestellingService> logger)
        {
            _service = service;
            _logger = logger;
        }

        // POST api/values
        [HttpPost]
        [SwaggerOperation("Post")]
        [ProducesResponseType(typeof(BestellingDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateBestelling([FromBody]BestellingDTO bestellingDTO)
        {
            if (!ModelState.IsValid)
            {
                var error = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");

                _logger.LogWarning("Create bestelling failed, Modelstate Invalide", bestellingDTO);
                return BadRequest(error);
            }

            try
            {
                //Bestelling bestelling = new Bestelling(bestellingDTO);
                //var response = _service.CreateBestelling(bestelling);

                //_logger.LogInformation("Create bestelling success", response);
                //return Ok(response);
                return Ok();
            }
            catch (Exception ex)
            {
                var error = new ErrorMessage(ErrorTypes.Unknown, $"Onbekende fout in create bestelling: {bestellingDTO},/nException: {ex}");

                _logger.LogError("Create bestelling failed, Unknown Error", bestellingDTO, ex);
                return BadRequest(error);
            }
        }
 
        [HttpPut]
        [SwaggerOperation("BestellingStatusOpgehaald")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult BestellingStatusOpgehaald([FromBody]long bestelling)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = _service.StatusNaarOpgehaald(bestelling);
                    return Ok(response);
                }
            }
            catch (DbUpdateException ex)
            {
                var error = new ErrorMessage(ErrorTypes.Unknown,
                        $"Fout met updaten in db: {bestelling}/nException: {ex}");
                _logger.LogError(error.FoutMelding);
                return NotFound(error);
            }
            catch (Exception ex)
            {
                var error = new ErrorMessage(ErrorTypes.Unknown,
                        $"Onbekende fout bij updaten: {bestelling}/nException: {ex}");
                return BadRequest(error);
            }
            var InvalidModelerror = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");
            return BadRequest(InvalidModelerror);
        }
        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}
