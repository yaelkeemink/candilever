using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAN.BackOffice.Agents.AuthenticatieAgents.Agents.Models;
using CAN.BackOffice.Agents.AuthenticatieAgents.Agents;

namespace CAN.BackOffice.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAuthenticatieService _authenticatieService;

        public LoginService(IAuthenticatieService authenticatieService )
        {
            _authenticatieService = authenticatieService;
        }
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<LoginResult> LoginAsync(LoginViewModel model)
        {

            var result = await _authenticatieService.BackOfficeLoginAsync(model);
            
            if(result is LoginResult)
            {
                return result as LoginResult;
            }

            return null;
        }
    }
}
