using CAN.BackOffice.Agents.AuthenticatieAgents.Agents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.BackOffice.Services
{
    public interface ILoginService
    {

       Task<LoginResult> LoginAsync(LoginViewModel model);
    }
}
