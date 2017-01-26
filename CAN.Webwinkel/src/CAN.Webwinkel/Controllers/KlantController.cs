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
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [SwaggerOperation("KlantAanmaken")]
        public IActionResult KlantAanmaken([FromBody]Klant klant)
        {
            //Very very dirty fix. Object from javascript on client-side has klantnummer = 0 before post is called. Somewhere between client and server side klantnummer
            //get corrupted.
            try
            {
                var response = _agent.Post(klant);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}