using System;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;
using Microsoft.EntityFrameworkCore;
using CAN.Klantbeheer.Domain.Domain.Entities;
using CAN.Klantbeheer.Facade.Facade.Errors;
using CAN.Klantbeheer.Domain.Domain.Services;

namespace CAN.Klantbeheer.Facade.Facade.Controllers
{
    [Route("api/[controller]")]
    public class KlantController : Controller
    {
        private readonly PlayerService _service;

        public KlantController(PlayerService service)
        {
            _service = service;
        }

        // POST api/values
        [HttpPost]        
        [SwaggerOperation("Post")]
        [ProducesResponseType(typeof(Klant), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreatePlayer([FromBody]Klant player)
        {
            if (!ModelState.IsValid)
            {
                var error = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");
                return BadRequest(error);
            }
                try
                {
                    var room = _service.CreatePlayer(player);
                    return Ok(room);
                }
                catch (Exception ex)
                {
                    var error = new ErrorMessage(ErrorTypes.Unknown,
                        $"Onbekende fout in create player: {player},/nException: {ex}");
                    return BadRequest(error);
                }


        }

        // PUT api/values/5
        [HttpPut]
        [SwaggerOperation("Update")]
        [ProducesResponseType(typeof(Klant), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdatePlayer([FromBody]Klant player)
        {
            if (ModelState.IsValid)
            {
                var error = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");
                return BadRequest(error);
            }
            try
            {
                var room = _service.UpdatePlayer(player);
                return Ok(room);
            }
            catch (DbUpdateException ex)
            {
                var error = new ErrorMessage(ErrorTypes.NotFound,
                        $"Fout met updaten in db: {player}/nException: {ex}");
                return NotFound(error);
            }
            catch (Exception ex)
            {
                var error = new ErrorMessage(ErrorTypes.Unknown,
                        $"Onbekende fout bij updaten: {player}/nException: {ex}");
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
