using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CryptoMaket.Managers;
using CryptoMaket.Models;
using Market.DAL;
using Market.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog;

namespace CryptoMaket.Controllers
{
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : Controller
    {
        private IConfiguration config;
        private readonly IUserService userService;
        private readonly IUserRolesService userRolesService;
        private readonly IUserManager userManager;
        private readonly Logger logger = LogManager.GetLogger("ExtendedLogging");

        public TokenController(IConfiguration config,
            IUserService userService,
            IUserRolesService userRolesService,
            IUserManager userManager)
        {
            this.config = config;
            this.userService = userService;
            this.userRolesService = userRolesService;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [HttpPost("login")]
        public async Task<IActionResult> CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = await this.userManager.Authenticate(login);

            if (user != null)
            {
                var credential = await this.userManager.BuildToken(user);
                response = Ok(new { credential });
            }
            else
            {
                this.logger.Error("FROM ExtendedLogging Invalid username or password");
                response = BadRequest(new { error = "Invalid UserName or Password!" });
            }

            return response;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Registration([FromBody] RegisterModel register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IActionResult response = BadRequest();
            try
            {
                if(await this.userManager.ValidateEmail(register.Email))
                {
                    return BadRequest(new { message = "Email already exists;"});
                }

                if (await this.userManager.ValidateUserName(register.UserName))
                {
                    return BadRequest(new { message = "UserName already exists;" });
                }

                User newUser = new User()
                {
                    Email = register.Email,
                    Password = register.Password,
                    UserName = register.UserName
                };
                await this.userService.AddUserAsync(newUser);
                response = Ok(new { message = "User registerd!" });
                return response;
            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message );
            }
        }
    }
}