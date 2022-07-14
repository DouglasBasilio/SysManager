﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderIntegrator.Application.Herlpers;
using SysManager.Application.Data.MySql;
using SysManager.Application.Data.MySql.Repositories;
using SysManager.Application.Services;
using System.Globalization;

namespace SysManager.API.Admin
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public void BeforeConfigureServices(IServiceCollection services)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            BeforeConfigureServices(services);
            services.AddApiVersioning();

            // injeção de dependência chamada Scope, define o ciclo de vida do nosso serviço
            services.AddScoped<UserService>();
            services.AddScoped<UserRepository>();

            services.AddScoped<ProductService>();
            services.AddScoped<ProductRepository>();

            services.AddScoped<UnityService>();
            services.AddScoped<UnityRepository>();

            services.AddScoped<ProductTypeService>();
            services.AddScoped<ProductTypeRepository>();

            services.AddScoped<CategoryService>();
            services.AddScoped<CategoryRepository>();

            services.AddScoped<MySqlContext>();
            services.Configure<AppConnectionSettings>(option => Configuration.GetSection("ConnectionStrings").Bind(option));

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
            
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var supportedCultures = new[] { new CultureInfo("en-US") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvc();
        }
    }
}
