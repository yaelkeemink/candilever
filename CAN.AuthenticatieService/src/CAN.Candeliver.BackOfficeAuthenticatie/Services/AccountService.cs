﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CAN.Candeliver.BackOfficeAuthenticatie.Controllers;
using CAN.Candeliver.BackOfficeAuthenticatie.Models;
using CAN.Candeliver.BackOfficeAuthenticatie.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CAN.Candeliver.BackOfficeAuthenticatie.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountController> _logger;
        private readonly TokenProviderOptions _options;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="options"></param>
        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory,
            IOptions<TokenProviderOptions> options, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _options = options.Value;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Create a jwt token for a given user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string CreateJwtTokenForUser(ApplicationUser user)
        {
            var now = DateTime.UtcNow;
            // Specifically add the jti (random nonce), iat (issued timestamp), and sub  (subject / user) claims.
            // You can add other claims here, if you want:
            var claims = new List<Claim>()
            {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.Ticks.ToString(),
                    ClaimValueTypes.Integer64),
            };

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: now,
            expires: now.Add(_options.Expiration),
            signingCredentials: _options.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> Register(string username, string password, string role)
        {
            var user = new ApplicationUser { UserName = username};

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                var addToRoleResult = await AddRole(user, role);
                if (addToRoleResult)
                {
                    _logger.LogInformation(3, "User created a new account with password.");
                    return user;
                } else
                {
                    _logger.LogInformation(3, "User created failed. deleting user");
                    await _userManager.DeleteAsync(user);
                }
            }
            return null;
        }


        /// <summary>
        /// Checks if user credentials are valid
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation(1, "User logged in.");
                return new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { });
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning(2, $"User {username} locked out.");
                return null;
            }
            else
            {
                _logger.LogWarning(2, $"Invalid user login for {username}");
                return null;
            }
        }


        /// <summary>
        /// Get a user. 
        /// </summary>
        /// <param name="userClaim"></param>
        /// <returns></returns>
        public Task<ApplicationUser> GetUserAsync(ClaimsPrincipal userClaim)
        {
            return _userManager.GetUserAsync(userClaim);
        }


        /// <summary>
        /// Add a role to a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> AddRole(ApplicationUser user, string role)
        {
            var userRole = await _roleManager.FindByNameAsync(role);
            if (userRole == null)
            {
                userRole = new IdentityRole(role);
                await _roleManager.CreateAsync(userRole);

            }

            if (!await _userManager.IsInRoleAsync(user, userRole.Name))
            {
                var result = await _userManager.AddToRoleAsync(user, userRole.Name);
                return result.Succeeded;
            }
            return true;
        }

      
    }
}
