using CAN.BackOffice.Agents.AuthenticatieAgents.Agents.Models;
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


    public class LoginController : BaseController
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService, ILogger<LoginController> logger) : base(logger)
        {
            _loginService = loginService;
        }

        [AllowAnonymous]
        [Authorize]
        public IActionResult LoginAction()
        {
            string cookie = Request.Cookies["access_token"];
            if (!string.IsNullOrWhiteSpace(cookie))
            {
                if (User.IsInRole("Sales"))
                {
                    return Redirect("/Sales/Index");
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
        public async Task<IActionResult> LoginAction(LoginModelDTO model)
        {
            var loginResult = await _loginService.LoginAsync(
                new LoginViewModel()
                {
                    UserName = model.UserName,
                    Password = model.Password
                });

            if (loginResult == null)
            {
                ModelState.AddModelError("LoginResult", "Combinatie van gebruikersnaam en wachtwoord is onjuist");
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
