using AutoMapper;
using dBankAPIMySQL.Controllers;
using dBankAPIMySQL.Data;
using dBankAPIMySQL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dBankAPIMySQL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContextPool<bankContext>(
               dbContextOptions => dbContextOptions
                   .UseMySql(
                       // Replace with your connection string.
                       "server=localhost;database=bank;user=root;treattinyasboolean=true",
                       // Replace with your server version and type.
                       // For common usages, see pull request #1233.
                       new MySqlServerVersion(new Version(10, 4, 14)), // use MariaDbServerVersion for MariaDB
                       mySqlOptions => mySqlOptions
                           .CharSetBehavior(CharSetBehavior.NeverAppend))
                   // Everything from this point on is optional but helps with debugging.
                   .EnableSensitiveDataLogging()
                   .EnableDetailedErrors());
            services.AddScoped<IUzytkownikRepo, SqlUzytkownikRepo>();
            services.AddScoped<IOperacjaRepo, SqlOperacjaRepo>();
            services.AddScoped<IRachunekRepo, SqlRachunekRepo>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMvc();
            services.AddCors(options => {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            })
        .AddJwtBearer("Bearer", jwtOptions =>
        {
            jwtOptions.TokenValidationParameters = new TokenValidationParameters()
            {
                // The SigningKey is defined in the TokenController class
                IssuerSigningKey = UzytkownikController.SIGNING_KEY,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(5)
            };
        });

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme);

                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();

          

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
