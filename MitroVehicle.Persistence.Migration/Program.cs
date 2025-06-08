using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MitroVehicle.Common;
using MitroVehicle.Persistence.Context;

namespace MitroVehicle.Persistence.Migration
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Configuration.Build(Directory.GetCurrentDirectory());

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConfiguration(Configuration.GetConfiguration().GetSection("Logging")).AddSimpleConsole();
            });
            var optionsBuilder = new DbContextOptionsBuilder<MitroVehicleContext>();
            optionsBuilder.UseLoggerFactory(loggerFactory).UseSqlServer(Configuration.ConnectionString);

            await new MitroVehicleContext(optionsBuilder.Options).Database.MigrateAsync();
        }
    }
}
