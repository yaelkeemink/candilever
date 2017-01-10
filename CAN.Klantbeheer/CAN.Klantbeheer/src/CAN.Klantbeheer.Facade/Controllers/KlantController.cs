using System;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;
using Microsoft.EntityFrameworkCore;
using CAN.Klantbeheer.Domain.Entities;
using CAN.Klantbeheer.Facade.Errors;
using CAN.Klantbeheer.Domain.Services;
using Microsoft.Extensions.Logging;

namespace CAN.Klantbeheer.Facade.Controllers
{
    [Route("api/[controller]")]
    public class KlantController : Controller
    {
        private readonly KlantService _service;
        private readonly ILogger<KlantController> _logger;

        public KlantController(ILogger<KlantController> logger, KlantService service)
        {
            _logger = logger;
            _service = service;
        }

        // POST api/values
        [HttpPost]
        [SwaggerOperation("Post")]
        [ProducesResponseType(typeof(Klant), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateKlant([FromBody]Klant klant)
        {
            _logger.LogInformation("Create Klant called", klant);
            if (!ModelState.IsValid)
            {
                var error = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");
                _logger.LogError("Create Klant Model State invalid", error);
                return BadRequest(error);
            }
            try
            {
                var room = _service.CreateKlant(klant);
                return Ok(room);
            }
            catch (Exception ex)
            {
                var error = new ErrorMessage(ErrorTypes.Unknown,
                    $"Onbekende fout in create player: {klant},/nException: {ex}");
                _logger.LogError("Create Klant unkown error occured", error);
                return BadRequest(error);
            }


        }

        // PUT api/values/5
        [HttpPut]
        [SwaggerOperation("Update")]
        [ProducesResponseType(typeof(Klant), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateKlant([FromBody]Klant klant)
        {
            _logger.LogInformation("Update Klant called", klant);
            if (!ModelState.IsValid)
            {
                var error = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");
                _logger.LogError("Update Klant Model State invalid", error);
                return BadRequest(error);
            }
            try
            {
                var room = _service.UpdateKlant(klant);
                return Ok(room);
            }
            catch (DbUpdateException ex)
            {
                var error = new ErrorMessage(ErrorTypes.NotFound,
                        $"Fout met updaten in db: {klant}/nException: {ex}");
                _logger.LogError("Update klant failed, klant not found", error);
                return NotFound(error);
            }
            catch (Exception ex)
            {
                var error = new ErrorMessage(ErrorTypes.Unknown,
                        $"Onbekende fout bij updaten: {klant}/nException: {ex}");
                _logger.LogError("Create Klant unkown error occured", error);
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
