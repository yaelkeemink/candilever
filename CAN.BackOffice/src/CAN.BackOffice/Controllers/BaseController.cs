using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CAN.BackOffice.Controllers
{
    
    public class BaseController 
        : Controller
    {
        protected readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected override void Dispose(bool disposing)
        {            
            base.Dispose(disposing);
        }
    }
}