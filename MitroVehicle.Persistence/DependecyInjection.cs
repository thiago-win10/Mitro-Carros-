using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MitroVehicle.Application.Common.Interfaces;
using MitroVehicle.Common;
using MitroVehicle.Persistence.Context;

namespace MitroVehicle.Persistence
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<MitroVehicleContext>(options =>
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

            services.AddScoped<IMitroVechicleContext, MitroVehicleContext>();

            return services;

        }

        public static IHealthChecksBuilder AddPersistenceHealthCheck(this IHealthChecksBuilder checksBuilder)
        {
            checksBuilder
                .AddSqlServer(
                    Configuration.ConnectionString,
                    name: "Database: MitroVehicle",
                    tags: new string[] { "MitroVehicle " });

            return checksBuilder;
        }
    }
}
