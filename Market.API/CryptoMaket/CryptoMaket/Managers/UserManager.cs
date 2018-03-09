using CryptoMaket.Models;
using Market.DAL;
using Market.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMaket.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IConfiguration config;
        private readonly IUserService userService;
        private readonly IUserRolesService userRolesService;
        private readonly IUserRefreshTokenService userRefreshTokenService;

        public UserManager(IConfiguration config,
            IUserService userService,
            IUserRolesService userRolesService,
            IUserRefreshTokenService userRefreshTokenService)
        {
            this.config = config;
            this.userService = userService;
            this.userRolesService = userRolesService;
            this.userRefreshTokenService = userRefreshTokenService;
        }

        public async Task<User> Authenticate(LoginModel login)
        {
            User user = await this.userService.GetUserByUsernameAndPassword(login.UserName, login.Password);
            return user;
        }

        public async Task<LoginResponseData> BuildToken(User user)
        {
            var userRoles = await this.userRolesService.GetUserRoles(user.Id);

            var tokenExists = await this.userRefreshTokenService.GetRefreshTokenByUserId(user.Id);

            if (tokenExists != null)
            {
                await this.userRefreshTokenService.RemoveRefreshToken(user.Id);
            }

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
              expires: DateTime.Now.AddMinutes(10),
              signingCredentials: creds);

            var access_token = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = Guid.NewGuid().ToString();

            await this.userRefreshTokenService.AddRefreshToken(user.Id, refreshToken);

            return new LoginResponseData
            {
                access_token = access_token,
                refresh_token = refreshToken,
                userId = user.Id,
                userName = user.UserName
            };
        }

        public async Task<bool> ValidateEmail(string email)
        {
            if (await this.userService.CheckIfEmailExists(email))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> ValidateUserName(string userName)
        {
            if (await this.userService.CheckIfUsernameExists(userName))
            {
                return true;
            }

            return false;
        }

        public async Task<LoginResponseData> RefreshToken(string refreshToken, int userId)
        {
            var userRefreshToken = await this.userRefreshTokenService.GetRefreshTokenByUserId(userId);
            if(!refreshToken.Equals(userRefreshToken.RefreshToken))
            {
                return null;
            }

            var user = await this.userService.GetUserById(userId);
            var userNewTokens = await this.BuildToken(user);

            return userNewTokens;
        }
    }
}
