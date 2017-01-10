using System;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;
using Microsoft.EntityFrameworkCore;
using CAN.Bestellingbeheer.Domain.Domain.Services;
using CAN.Bestellingbeheer.Facade.Facade.Errors;
using CAN.Bestellingbeheer.Domain.Domain.Entities;

namespace CAN.Bestellingbeheer.Facade.Facade.Controllers
{
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        private readonly PlayerService _service;

        public PlayerController(PlayerService service)
        {
            _service = service;
        }

        // POST api/values
        [HttpPost]        
        [SwaggerOperation("Post")]
        [ProducesResponseType(typeof(Player), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreatePlayer([FromBody]Player player)
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
        [ProducesResponseType(typeof(Player), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdatePlayer([FromBody]Player player)
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
