using BusinessInfo.Application.Common.Interfaces;
using BusinessInfo.Common;
using BusinessInfo.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessInfo.Persistence
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<BusinessInfoContext>(options =>
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

            services.AddScoped<IBusinessInfoContext, BusinessInfoContext>();

            return services;

        }

        public static IHealthChecksBuilder AddPersistenceHealthCheck(this IHealthChecksBuilder checksBuilder)
        {
            checksBuilder
                .AddSqlServer(
                    Configuration.ConnectionString,
                    name: "Database: BusinessInfo",
                    tags: new string[] { "BusinessInfo " });

            return checksBuilder;
        }
    }
}
