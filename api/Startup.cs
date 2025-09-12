using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using domain.Interface;
using data.Repositories;
using domain.Data;
using MediatR;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("AllowAngularApp",
                    options => options.WithOrigins("http://localhost:4200", "https://localhost:4200")
                                     .AllowAnyMethod()
                                     .AllowAnyHeader()
                                     .AllowCredentials());
            });

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Assurance Santé",
                    Version = "v1",
                    Description = "API de gestion des assurances santé"
                });
            });

            // EF Core avec votre contexte
            services.AddDbContext<AssuranceDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Connection"),
                    sqlOptions => sqlOptions.MigrationsAssembly("data"));
            });

            // MediatR - Configuration pour .NET Core 3.1
            services.AddMediatR(
                Assembly.GetExecutingAssembly(),
                typeof(domain.Handlers.GetGenericHandlers<>).Assembly,
                typeof(domain.Commands.AddGenericCommand<>).Assembly,
                //il ya quelque chose qui cloche ici
                typeof(domain.Queries.GetGenericQuery).Assembly
            );

            // Enregistrement des services
            RegisterServices(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            // Repository générique
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Repositories spécifiques
            services.AddScoped<IAdherentRepository, AdherentRepository>();
            services.AddScoped<IAgentRepository, AgentRepository>();
            services.AddScoped<IDemandeAdhesionRepository, DemandeAdhesionRepository>();
            services.AddScoped<ICotisationRepository, CotisationRepository>();
            services.AddScoped<IFactureRepository, FactureRepository>();
            services.AddScoped<IPaiementRepository, PaiementRepository>();
            services.AddScoped<IDemandeContreVisiteRepository, DemandeContreVisiteRepository>();
            services.AddScoped<IRapportMedicalRepository, RapportMedicalRepository>();
            services.AddScoped<IMedecinControleRepository, MedecinControleRepository>();
            services.AddScoped<IPlanSanteRepository, PlanSanteRepository>();
            services.AddScoped<IReclamationRepository, ReclamationRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // CORS après UseRouting()
            app.UseCors("AllowAngularApp");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Assurance Santé v1");
                c.RoutePrefix = "swagger";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    context.Response.Redirect("/swagger");
                });
            });

            // Middleware de gestion d'erreurs global
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var errorResponse = new
                    {
                        error = "Une erreur interne s'est produite",
                        message = ex.Message,
                        stackTrace = env.IsDevelopment() ? ex.StackTrace : null
                    };

                    var json = JsonSerializer.Serialize(errorResponse);
                    await context.Response.WriteAsync(json);
                }
            });
        }
    }
}