using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CAN.Webwinkel.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CategorieController : Controller
    {
        private readonly ILogger<CategorieController> _logger;

        public CategorieController(ILogger<CategorieController> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Object> Get()
        {
            return null;
        }
    }
}