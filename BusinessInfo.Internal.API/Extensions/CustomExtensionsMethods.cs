using BusinessInfo.Common;
using BusinessInfo.Internal.API.Filters;
using BusinessInfo.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BusinessInfo.Internal.API.Extensions
{
    public static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomOpenAPI(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api dos Emissores de Veiculos", Version = "v1" });

                // Configuração do Bearer
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT desta forma: Bearer {seu token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });

            return services;
        }

        public static IServiceCollection AddCustomFramework(this IServiceCollection services)
        {

            services.AddControllers(opt =>
            {
                opt.Filters.Add(typeof(ValidateModelStateAttribute));
            })
            .AddNewtonsoftJson(options =>

                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            )
            .AddJsonOptions(option =>
                option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
            )
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<string>();
            });



            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSingleton(option =>
            {
                return Configuration._configuration;
            });

            services.AddHeaderPropagation(options =>
            {
                options.Headers.Add("X-TraceId");
            });

            return services;
        }

        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
        {
            var hcBuilder = services.AddHealthChecks();
            hcBuilder.AddPersistenceHealthCheck();

            return services;
        }
    }
}
