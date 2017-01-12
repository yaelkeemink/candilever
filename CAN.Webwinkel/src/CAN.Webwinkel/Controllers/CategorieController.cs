using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CAN.Webwinkel.Domain.Interfaces;
using System.Net;
using Swashbuckle.SwaggerGen.Annotations;

namespace CAN.Webwinkel.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CategorieController : Controller
    {
        private readonly ILogger<CategorieController> _logger;
        private readonly ICategorieService _service;

        public CategorieController(ILogger<CategorieController> logger, ICategorieService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [SwaggerOperation("AlleCategorieen")]
        public IActionResult Get()
        {
            return Json(_service.AlleCategorieen());
        }
    }
}