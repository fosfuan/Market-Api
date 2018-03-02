using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CryptoMaket.Models;
using Market.DAL;
using Market.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CryptoMaket.Controllers
{
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : Controller
    {
        private IConfiguration config;
        private readonly IUserService userService;
        private readonly IUserRolesService userRolesService;

        public TokenController(IConfiguration config,
            IUserService userService,
            IUserRolesService userRolesService)
        {
            this.config = config;
            this.userService = userService;
            this.userRolesService = userRolesService;
        }

        [AllowAnonymous]
        [HttpPost]
        [HttpPost("login")]
        public async Task<IActionResult> CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = await Authenticate(login);

            if (user.UserName != null)
            {
                var tokenString = await BuildToken(user);
                response = Ok(new { token = tokenString });
            }
            else
            {
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
                if(await this.userService.CheckIfEmailExists(register.Email))
                {
                    return BadRequest(new { message = "Email already exists;"});
                }

                if (await this.userService.CheckIfUsernameExists(register.UserName))
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

        private async Task<string> BuildToken(User user)
        {
            var userRoles = await this.userRolesService.GetUserRoles(user.Id);

            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, userRole));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(this.config["Jwt:Issuer"],
              this.config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> Authenticate(LoginModel login)
        {
            User user = await this.userService.GetUserByUsernameAndPassword(login.UserName, login.Password);
            
            return user;
        }

    }
}