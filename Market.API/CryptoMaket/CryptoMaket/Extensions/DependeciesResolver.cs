using CryptoMaket.Managers;
using Market.DAL.Repositories;
using Market.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoMaket.Extensions
{
    public static class DependeciesResolver
    {
        public static void Resolve(TinyIoCContainer container, IConfiguration configuration)
        {
            //container.Register<IUserRepository>(new UserRepository(configuration.GetConnectionString("DefaultConnection")));
            //container.Register<IUserService>(new UserService(container.Resolve<IUserRepository>()));
        }

        public static void Resolve(IServiceCollection services, IConfiguration configration)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            //services.AddTransient<IUserRoleRepository, UserRoleRepository>();

            //services.AddTransient<ICoinsRepository, CoinsRepository>();
            services.AddTransient<ICoinService, CoinService>();

            services.AddTransient<IUserRolesService, UserRolesService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IUserRefreshTokenService, UserRefreshTokenService>();
            services.AddTransient<IUserRefreshTokenRepository, UserRefreshTokenRepository>();

            services.AddTransient<IUserManager, UserManager>();
        }
    }
}
