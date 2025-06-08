using Microsoft.EntityFrameworkCore;
using MitroVehicle.Application;
using MitroVehicle.Application.Common.Interfaces;
using MitroVehicle.Common;
using MitroVehicle.Persistence.Context;
using MitroVehicle.UpdateStatusLocation;


Configuration.Build(Directory.GetCurrentDirectory());

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MitroVehicleContext>(options =>
{
    options.UseSqlServer(Configuration.ConnectionString,
        sqlServerOptionsAction: sqlOoptions =>
        {
            sqlOoptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
        });
},
               ServiceLifetime.Scoped
           );

builder.Services.AddScoped<IMitroVechicleContext, MitroVehicleContext>();
builder.Services.AddApplication();
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
