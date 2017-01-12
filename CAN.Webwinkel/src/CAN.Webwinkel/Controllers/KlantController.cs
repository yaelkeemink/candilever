using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;
using CAN.Webwinkel.Agents.KlantAgent;
using CAN.Webwinkel.Agents.KlantAgent.Models;

namespace CAN.Webwinkel.Controllers
{
    [Route("api/[controller]")]
    public class KlantController : Controller
    {
        private readonly IKlantAgent _agent;

        public KlantController(IKlantAgent agent)
        {
            _agent = agent;
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [SwaggerOperation("KlantAanmaken")]
        public IActionResult KlantAanmaken([FromBody]Klant klant)
        {
            try
            {
                _agent.Post(klant);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}