using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CAN.Webwinkel.Agents.WinkelwagenAgent;
using System.Net;
using Swashbuckle.SwaggerGen.Annotations;
using CAN.Webwinkel.Agents.WinkelwagenAgent.Models;
using CAN.Webwinkel.Domain.Interfaces;

namespace CAN.Webwinkel.Controllers
{
    [Route("api/[controller]")]
    public class WinkelmandjeController : Controller
    {
        private readonly IWinkelwagenAgentClient _agent;
        private readonly IWinkelwagenService _winkelwagenService;
        private readonly IArtikelService _artikelService;


        public WinkelmandjeController(IWinkelwagenAgentClient agent, 
            IWinkelwagenService winkelwagenService, 
            IArtikelService artikelService)
        {
            _agent = agent;
            _winkelwagenService = winkelwagenService;
            _artikelService = artikelService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [SwaggerOperation("WinkelwagenAanmaken")]
        public IActionResult WinkelwagenAanmaken([FromBody]Winkelmandje winkelmandje)
        {
            if (ModelState.IsValid)
            {
                try
                {                    
                    winkelmandje.Artikelen.First().Prijs = _artikelService.FindArtikelByArtikelNummer((long)winkelmandje.Artikelen.Single().Artikelnummer);
                    var apiResponse = _agent.Post(winkelmandje);
                    if (apiResponse is Winkelmandje)
                    {
                        var winkelmandjeModel = apiResponse as Winkelmandje;
                        var mapping = Mappers.WinkelmandjeMapper.Map(winkelmandjeModel);
                        _winkelwagenService.Insert(mapping);
                        return Ok(mapping.WinkelmandjeNummer);
                    }
                    return BadRequest(apiResponse);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            } else
            {
                return BadRequest("Modelstate invalid");
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [SwaggerOperation("WinkelwagenUpdaten")]
        public IActionResult WinkelwagenUpdaten([FromBody]Winkelmandje winkelmandje)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var apiResponse = _agent.Post(winkelmandje);
                    if (apiResponse is Winkelmandje)
                    {
                        var winkelmandjeModel = apiResponse as Winkelmandje;
                        var mapping = Mappers.WinkelmandjeMapper.Map(winkelmandjeModel);
                        _winkelwagenService.Update(mapping);
                        return Ok(mapping.WinkelmandjeNummer);
                    }
                    return BadRequest(apiResponse);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            } else
            {
                return BadRequest("Modelstate invalid");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [SwaggerOperation("WinkelwagenAfronden")]
        [Route("Finish")]
        public IActionResult WinkelwagenAfronden([FromBody]Bestelling bestelling)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _agent.Afronden(bestelling);
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            } else
            {
                return BadRequest("Modelstate invalid");
            }
        }
    }
}