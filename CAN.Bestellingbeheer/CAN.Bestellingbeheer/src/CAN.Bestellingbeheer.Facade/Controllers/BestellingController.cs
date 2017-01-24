using System;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;
using Microsoft.EntityFrameworkCore;
using CAN.Bestellingbeheer.Facade.Errors;
using Microsoft.Extensions.Logging;
using CAN.Bestellingbeheer.Infrastructure.Interfaces;

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
 
        [HttpPut]
        [SwaggerOperation("BestellingStatusOpgehaald")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult BestellingStatusOpgehaald([FromBody]long bestelling)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = _service.StatusNaarOpgehaald(bestelling).Status.ToString();
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
