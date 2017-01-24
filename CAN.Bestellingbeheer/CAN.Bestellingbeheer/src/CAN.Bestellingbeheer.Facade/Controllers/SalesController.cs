using CAN.Bestellingbeheer.Domain.Entities;
using CAN.Bestellingbeheer.Facade.Errors;
using CAN.Bestellingbeheer.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Swashbuckle.SwaggerGen.Annotations;
using System;
using System.Net;

namespace CAN.Bestellingbeheer.Facade.Controllers
{
    [Route("api/[controller]")]
    public class SalesController
        : Controller
    {
        private readonly IBestellingService _service;
        private readonly ILogger<IBestellingService> _logger;

        public SalesController(IBestellingService service, ILogger<IBestellingService> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPut]
        [SwaggerOperation("BestellingGoedkeuren")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult BestellingGoedkeuren([FromBody]long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Bestelling response = _service.StatusNaarGoedgekeurd(id);
                    return Ok(response.Status.ToString());
                }
            }
            catch (DbUpdateException ex)
            {
                var error = new ErrorMessage(ErrorTypes.Unknown,
                        $"Fout met updaten in db: {id}/nException: {ex}");
                _logger.LogError(error.FoutMelding);
                return NotFound(error);
            }
            catch (Exception ex)
            {
                var error = new ErrorMessage(ErrorTypes.Unknown,
                        $"Onbekende fout bij updaten: {id}/nException: {ex}");
                return BadRequest(error);
            }
            var InvalidModelerror = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");
            return BadRequest(InvalidModelerror);
        }
        [HttpPost]
        [SwaggerOperation("BestellingAfkeuren")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult BestellingAfkeuren([FromBody]long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Bestelling response = _service.StatusNaarAfgekeurd(id);
                    return Ok(response.Status.ToString());
                }
            }
            catch (DbUpdateException ex)
            {
                var error = new ErrorMessage(ErrorTypes.Unknown,
                        $"Fout met updaten in db: {id}/nException: {ex}");
                _logger.LogError(error.FoutMelding);
                return NotFound(error);
            }
            catch (Exception ex)
            {
                var error = new ErrorMessage(ErrorTypes.Unknown,
                        $"Onbekende fout bij updaten: {id}/nException: {ex}");
                return BadRequest(error);
            }
            var InvalidModelerror = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");
            return BadRequest(InvalidModelerror);
        }
    }
}
