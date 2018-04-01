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
    [Route("api/user")]
    public class UserController : Controller
    {
        private IConfiguration config;
        private readonly IUserService userService;
        private readonly IUserRolesService userRolesService;
        private readonly IUserManager userManager;

        public UserController(IConfiguration config,
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IActionResult response = Unauthorized();
            var user = await this.userManager.Authenticate(login);

            if (user != null)
            {
                var credential = await this.userManager.BuildToken(user);
                response = Ok(new { credential });
            }
            else
            {
                response = BadRequest("Invalid UserName or Password!");
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

        [Authorize(Roles = "BasicUser")]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel refreshTokenModel)
        {
            IActionResult result = null;
            try
            {
                var newRefreshTokens = await this.userManager.RefreshToken(refreshTokenModel.RefreshToken, refreshTokenModel.UserId);
                if(newRefreshTokens == null)
                {
                    return BadRequest("Inwalid Refresh Token!");
                }
                result = Ok(new { newRefreshTokens });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return result;
        }

    }
}