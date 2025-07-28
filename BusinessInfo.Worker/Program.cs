using BusinessInfo.Application;
using BusinessInfo.Application.Common.Interfaces;
using BusinessInfo.Common;
using BusinessInfo.Persistence.Context;
using Microsoft.EntityFrameworkCore;


Configuration.Build(Directory.GetCurrentDirectory());

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BusinessInfoContext>(options =>
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

builder.Services.AddScoped<IBusinessInfoContext, BusinessInfoContext>();
builder.Services.AddApplication();
//builder.Services.AddHostedService<Worker>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
