using System;
using System.Reflection;
using DomainEntities = DGT.Data.Domain.Entities;
using Models = DGT.Api.Models;
using DGT.Data.Infrastructure;
using DGT.Data.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DGT.Api
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ConfigureSwagger(services);

            ConfigureEntityFramework(services);

            services.AddScoped<IDgtRepository, DgtRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            // Configuracion de Automapper
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Models.VehiculoDto, DomainEntities.Vehiculo>();
                cfg.CreateMap<Models.ConductorCreacionDto, DomainEntities.Conductor>().ForMember(model => model.VehiculosHabituales, opt => opt.Ignore());
                cfg.CreateMap<Models.InfraccionCreacionDto, DomainEntities.Infraccion> ();
                cfg.CreateMap<Models.TipoInfraccionDto, DomainEntities.TipoInfraccion> ();
            });

            app.UseMvc();
        }

        public void ConfigureEntityFramework(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DgtContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DGT_DB"),
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(DgtContext).GetTypeInfo().Assembly.GetName().Name);
                            sqlOptions.UseRowNumberForPaging();
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        });
                },
                    ServiceLifetime.Scoped
                );
        }

        public void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(n => n.FullName);
                options.DescribeAllEnumsAsStrings();

                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Data HTTP API",
                    Version = "v1",
                    Description = "The Data HTTP API",
                    TermsOfService = "Terms Of Service"
                });

                options.AddSecurityDefinition("ApiKey", new Swashbuckle.AspNetCore.Swagger.ApiKeyScheme
                {
                    Description = "ApiKey header. Example: \"api-key: {password}\"",
                    Name = "api-key",
                    In = "header",
                    Type = "apiKey"
                });
            });
        }
    }
}
