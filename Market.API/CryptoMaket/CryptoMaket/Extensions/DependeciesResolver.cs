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
            container.Register<IUserRepository>(new UserRepository(configuration.GetConnectionString("DefaultConnection")));
            container.Register<IUserService>(new UserService(container.Resolve<IUserRepository>()));
        }

        public static void Resolve(IServiceCollection services, IConfiguration configration)
        {
            services.AddTransient<IUserRepository, UserRepository>(serviceProvider =>
            {
                return new UserRepository(configration.GetConnectionString("DefaultConnection"));
            });
            services.AddTransient<IUserService, UserService>();
        }
    }
}
