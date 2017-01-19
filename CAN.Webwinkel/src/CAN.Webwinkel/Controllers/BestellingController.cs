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
    [Route("api/[controller]")]
    public class BestellingController : Controller
    {
        private readonly IBestellingsBeheerAgent _agent;

        public BestellingController(IBestellingsBeheerAgent agent)
        {
            _agent = agent;
        }


        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [SwaggerOperation("BestellingPlaatsen")]
        public IActionResult BestellingGeplaatst([FromBody]BestellingDTO bestelling)
        {
            bestelling.Validate();
            try
            {
                var response = _agent.Post(bestelling);
                if (response is ErrorMessage)
                {
                    return BadRequest(response as ErrorMessage);
                }
                return Ok(response as BestellingDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}