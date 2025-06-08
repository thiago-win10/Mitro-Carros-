using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MitroVehicle.Application;
using MitroVehicle.Application.Common.AES;
using MitroVehicle.Application.Common.Interfaces;
using MitroVehicle.Application.Common.Models.Response;
using MitroVehicle.Common;
using MitroVehicle.Internal.API.Extensions;
using MitroVehicle.Internal.API.Filters;
using MitroVehicle.Internal.API.Middlewares;
using MitroVehicle.Persistence;
using MitroVehicle.Persistence.Context;
using System.Reflection;
using System.Text;

namespace MitroVehicle.Internal.API;

public class Program
{
    public static void Main(string[] args)
    {
        Configuration.Build(Directory.GetCurrentDirectory());

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplication();
        builder.Services.AddCustomFramework();
        builder.Services.AddControllers();
        builder.Services.AddCustomOpenAPI();
        builder.Services.AddCustomHealthChecks();
        builder.Services.AddHealthChecks();
        builder.Services.AddPersistence();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(x =>
        {
            x.SwaggerEndpoint("/swagger/v1/swagger.json", "Internal.Api v1");
            x.RoutePrefix = "docs";
        });

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCustomExceptionHandler();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health");
            endpoints.MapGet("/{**path}", async context =>
            {
                await context.Response.WriteAsync(
                    "Navigate to /health to see the health status.");
            });
            endpoints.MapControllers();
        });

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
        });
        app.Urls.Add("http://*:5001");
        app.Run();


    }

}


