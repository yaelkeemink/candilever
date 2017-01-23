using System;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;
using Microsoft.EntityFrameworkCore;
using CAN.WinkelmandjeBeheer.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using CAN.WinkelmandjeBeheer.Facade.Facade.Errors;
using CAN.WinkelmandjeBeheer.Domain.Domain.Entities;

namespace CAN.WinkelmandjeBeheer.Facade.Facade.Controllers
{
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        private readonly IWinkelmandjeService _service;

        public PlayerController(IWinkelmandjeService service, ILogger<IWinkelmandjeService> logger)
        {
            _service = service;
        }

        // POST api/values
        [HttpPost]        
        [SwaggerOperation("Post")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
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
                    var room = _service.CreateWinkelmandje(winkelmandje);
                    return Ok(room);
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
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdatePlayer([FromBody]Winkelmandje winkelmandje)
        {
            if (ModelState.IsValid)
            {
                var error = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");
                return BadRequest(error);
            }
            try
            {
                var room = _service.UpdateWinkelmandje(winkelmandje);
                return Ok(room);
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
        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}
