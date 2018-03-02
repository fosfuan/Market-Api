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

        public TokenController(IConfiguration config,
            IUserService userService)
        {
            this.config = config;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
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

                User user = new User()
                {
                    Email = register.Email,
                    Password = register.Password,
                    UserName = register.UserName
                };
                await this.userService.AddUserAsync(user);
                response = Ok(new { message = "User registerd!" });
                return response;
            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message );
            }
        }

        private string BuildToken(UserModel user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "Test", ClaimValueTypes.String),
                new Claim(ClaimTypes.DateOfBirth, "2017-06-08", ClaimValueTypes.Date)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(this.config["Jwt:Issuer"],
              this.config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(LoginModel login)
        {
            UserModel user = null;

            if (login.Email == "wojtek" && login.Password == "secret")
            {
                user = new UserModel { UserName = "Wojtek Krawiec", Email = "wojtek.krawiec@domain.com" };
            }
            return user;
        }

    }
}