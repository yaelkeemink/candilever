﻿using CAN.BackOffice.Agents.AuthenticatieAgents.Agents.Models;
using CAN.BackOffice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using CAN.BackOffice.Models;

namespace CAN.BackOffice.Controllers
{


    public class LoginController : Controller
    {
        private readonly ILogger _logger;
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService, ILogger<LoginController> logger)
        {
            _loginService = loginService;
            _logger = logger;
        }

        [AllowAnonymous]
        [Authorize]
        public IActionResult LoginAction()
        {
            _logger.LogInformation($"cookie " + Request.Cookies["access_token"]);
            string cookie = Request.Cookies["access_token"];
            if (!string.IsNullOrWhiteSpace(cookie))
            {

                if (User.IsInRole("Sales"))
                {
                    return Redirect("/Sales");
                }
                else if (User.IsInRole("Magazijn"))
                {
                    return Redirect("/Magazijn/BestellingOphalen");
                }

            }

            
            return View(new LoginModelDTO());

        }

        /// <summary>
        /// Post request for login. 
        /// If succesful it wil set a acces token cookie. if not it wil show an a error
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult LoginAction(LoginModelDTO model)
        {
            var x = 0;
            var loginResult = _loginService.LoginAsync(new LoginViewModel() { UserName = model.UserName, Password = model.Password}).Result;

            if (loginResult == null)
            {
                ModelState.AddModelError("LoginResult", "Combinatie van gebruikersnaam en wachtwoord is incorrect");
                return View(model);
            }

            Response.Cookies.Append(
              "access_token",
              loginResult.AccessToken,
              new CookieOptions()
              {
                  Path = "/",
                  HttpOnly = false,
                  Secure = false,
                  Expires = new DateTimeOffset(DateTime.Now.AddSeconds(loginResult.ExpiresIn.Value))
              });
            return Redirect("/Login");
        }


    }
}
