using BusinessInfo.Common;
using BusinessInfo.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessInfo.Persistence.Migration
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
            var optionsBuilder = new DbContextOptionsBuilder<BusinessInfoContext>();
            optionsBuilder.UseLoggerFactory(loggerFactory).UseSqlServer(Configuration.ConnectionString);

            await new BusinessInfoContext(optionsBuilder.Options).Database.MigrateAsync();
        }
    }
}
