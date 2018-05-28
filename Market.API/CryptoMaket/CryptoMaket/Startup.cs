using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoMaket.EFMarket_DAL.Models.DB;
using CryptoMaket.Extensions;
using CryptoMaket.Filter;
using CryptoMaket.Handler;
using EFMarket.DAL;
using EFMarket.DAL.UnitOfWork;
using Market.DAL.Repositories;
using Market.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nancy.TinyIoc;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;

namespace CryptoMaket
{
    public class Startup
    {
        private static TinyIoCContainer container = TinyIoCContainer.Current;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DependeciesResolver.Resolve(services, Configuration);
            
            services.AddCors();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = Configuration["Jwt:Issuer"],
                   ValidAudience = Configuration["Jwt:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                   RequireExpirationTime = true                   
               };
           });
            services.AddMvc();
            services.AddScoped<TestFilter>();
            services.AddDbContext<CryptoMarketContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging")); //log levels set in your configuration
            //loggerFactory.AddDebug(); //does all log levels
            //loggerFactory.AddFile(Configuration["LogFile"], LogLevel.Information);
            app.UseCors(builder => builder
                .WithOrigins(Configuration["MarketApi:Url"])
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<RequestLogHandler>();
            app.UseMiddleware<ReponseLogHandler>();
            app.UseMiddleware<ExceptionResponseHandler>(); 
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}


