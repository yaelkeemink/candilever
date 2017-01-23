using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.BackOffice.Controllers
{
    [Route("api/v1/[controller]")]
    public class LoginController : Controller
    {
        [Route("Test")]
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation("TestPublic")]
        public string test()
        {
            return "hello public";
        }
        [Route("Test2")]
        [Authorize]
        [HttpGet]
        [SwaggerOperation("TestPrivate")]
        public string test2()
        {
            return "hello private";
        }


        [Route("Test3")]
        [Authorize(Roles ="Sales")]
        [HttpGet]
        [SwaggerOperation("TestPrivateSales")]
        public string test3()
        {
            return "hello sales";
        }

    }
}
