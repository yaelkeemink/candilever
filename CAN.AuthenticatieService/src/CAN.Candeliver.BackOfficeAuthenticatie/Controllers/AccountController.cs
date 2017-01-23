using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using CAN.Candeliver.BackOfficeAuthenticatie.Models;
using CAN.Candeliver.BackOfficeAuthenticatie.Models.AccountViewModels;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IdentityModel.Tokens.Jwt;
using CAN.Candeliver.BackOfficeAuthenticatie.Security;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CAN.Candeliver.BackOfficeAuthenticatie.Services;

namespace CAN.Candeliver.BackOfficeAuthenticatie.Controllers
{
    [Route("api/v1/[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly TokenProviderOptions _options;
        private readonly IAccountService _accountService;

        public AccountController(
            ILogger<AccountController> logger,
            IOptions<TokenProviderOptions> options, IAccountService accountService)
        {
            _logger = logger;
            _options = options.Value;
            _accountService = accountService;
        }


        // POST: /Account/Login
        [HttpPost]
        [Route("Login")]
        [SwaggerOperation("BackOfficeLogin")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad credentials");
            }
            try
            {
                var identity = await _accountService.GetIdentityAsync(model.UserName, model.Password);
                if (identity == null)
                {
                    return BadRequest("Invalid username or password.");
                }

                var user = await _accountService.GetUserAsync(User);
                var response = new LoginResult()
                {
                    Access_Token = _accountService.CreateJwtTokenForUser(user),
                    Expires_In = (int)_options.Expiration.TotalSeconds

                };
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"Login user failed: {e.Message}");
                _logger.LogDebug($"Login user failed: {e.StackTrace}");
                return BadRequest("User login exception");
            }


        }
        // POST: /Account/Register
        [HttpPost]
        [Route("Register")]
        [SwaggerOperation("BackOfficeRegister")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }

            try
            {
                var result = await _accountService.Register(model.UserName, model.Email, model.Password, model.Role);
                if (result == null)
                {
                    return BadRequest("User registration failed");
                }
                return Json("User created a new account with password.");

            }
            catch (Exception e)
            {
                _logger.LogError($"Creating user failed: {e.Message}");
                _logger.LogDebug($"Creating user failed: {e.StackTrace}");
                return BadRequest("User registration exception");
            }
        }      
        
    }
}
