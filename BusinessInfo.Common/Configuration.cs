using Microsoft.Extensions.Configuration;

namespace BusinessInfo.Common
{
    public static class Configuration
    {
        public static IConfigurationRoot _configuration;
        public static void Build(string pathJsonFile)
        {
            var environment1 = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            var environment2 = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .SetBasePath(pathJsonFile)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddJsonFile($"appsettings.{environment1}.json", optional: true, reloadOnChange: false)
                .AddJsonFile($"appsettings.{environment2}.json", optional: true, reloadOnChange: false)
                .AddJsonFile($"secret/appsettings.secrets.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
            _configuration = builder.Build();


        }

        public static IConfiguration GetConfiguration()
        {
            return _configuration;
        }

        public static List<RequestPerfomanceBehaviourSetting> RequestPerfomanceBehaviourSettings
        {
            get
            {
                var list = new List<RequestPerfomanceBehaviourSetting>();
                try
                {
                    _configuration.GetSection("RequestPerfomanceBehaviourSetting")?.Bind(list);
                }
                catch
                {

                }
                if (!list.Any())
                    list.Add(new RequestPerfomanceBehaviourSetting() { Resource = "Default", ExecutionLimit = 1000 });

                return list;
            }
        }

        public static string ConnectionString => _configuration.GetConnectionString("BusinessInfoConncetionString");
        public static string EncryptionKey => _configuration.GetSection("EncryptionSettings")["Key"];
        public static string EncryptionAESPath => _configuration.GetSection("Encryption")["Key"];
        public static string EncryptionAESIVPath => _configuration.GetSection("Encryption")["IV"];
        public static string RedisConnection => _configuration.GetSection("Redis")["RedisConnection"];




    }
}
