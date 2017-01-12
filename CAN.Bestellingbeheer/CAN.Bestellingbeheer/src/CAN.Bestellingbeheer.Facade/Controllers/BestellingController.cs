﻿using System;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;
using Microsoft.EntityFrameworkCore;
using CAN.Bestellingbeheer.Domain.Services;
using CAN.Bestellingbeheer.Domain.Entities;
using CAN.Bestellingbeheer.Facade.Errors;
using Microsoft.Extensions.Logging;

namespace CAN.Bestellingbeheer.Facade.Controllers
{
    [Route("api/[controller]")]
    public class BestellingController : Controller
    {
        private readonly BestellingService _service;
        private readonly ILogger<BestellingService> _logger;

        public BestellingController(BestellingService service, ILogger<BestellingService> logger)
        {
            _service = service;
            _logger = logger;
        }

        // POST api/values
        [HttpPost]        
        [SwaggerOperation("Post")]
        [ProducesResponseType(typeof(Bestelling), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateBestelling([FromBody]Bestelling bestelling)
        {
            if (!ModelState.IsValid)
            {
                var error = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");

                _logger.LogWarning("Create bestelling failed, Modelstate Invalide", bestelling);
                return BadRequest(error);
            }

            try
            {
                var result = _service.CreateBestelling(bestelling);

                _logger.LogInformation("Create bestelling success", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var error = new ErrorMessage(ErrorTypes.Unknown, $"Onbekende fout in create bestelling: {bestelling},/nException: {ex}");

                _logger.LogError("Create bestelling failed, Unknown Error", bestelling, ex);
                return BadRequest(error);
            }
        }

        // PUT api/values/5
        [HttpPut]
        [SwaggerOperation("Update")]
        [ProducesResponseType(typeof(Bestelling), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateBestelling([FromBody]Bestelling bestelling)
        {
            if (!ModelState.IsValid)
            {
                var error = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");
                return BadRequest(error);
            }
            try
            {
                var room = _service.UpdateBestelling(bestelling);
                return Ok(room);
            }
            catch (DbUpdateException ex)
            {
                var error = new ErrorMessage(ErrorTypes.NotFound,
                        $"Fout met updaten in db: {bestelling}/nException: {ex}");
                return NotFound(error);
            }
            catch (Exception ex)
            {
                var error = new ErrorMessage(ErrorTypes.Unknown,
                        $"Onbekende fout bij updaten: {bestelling}/nException: {ex}");
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