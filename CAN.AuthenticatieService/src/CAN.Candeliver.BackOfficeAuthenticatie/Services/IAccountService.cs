using CAN.Candeliver.BackOfficeAuthenticatie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CAN.Candeliver.BackOfficeAuthenticatie.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string CreateJwtTokenForUser(ApplicationUser user);
        
        Task<ApplicationUser> Register(string username, string email, string password, string role);
        Task<ClaimsIdentity> GetIdentityAsync(string username, string password);
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal userClaim);

        Task<bool> AddRole(ApplicationUser user, string role);
    }
}
