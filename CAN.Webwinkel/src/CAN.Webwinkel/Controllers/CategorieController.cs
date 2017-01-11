using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CAN.Webwinkel.Domain.Interfaces;

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

        public IEnumerable<string> Get()
        {
            return _service.AlleCategorieen();
        }
    }
}