using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;
using CAN.Webwinkel.Agents.BestellingsAgent;
using CAN.Webwinkel.Agents.BestellingsAgent.Models;

namespace CAN.Webwinkel.Controllers
{
    public class BestellingController : Controller
    {
        private readonly IBestellingsAgent _agent;

        public BestellingController(IBestellingsAgent agent)
        {
            _agent = agent;
        }


        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [SwaggerOperation("BestellingPlaatsen")]
        public IActionResult BestellingGeplaatst([FromBody]Bestelling bestelling)
        {
            try
            {
                _agent.Post(bestelling);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}