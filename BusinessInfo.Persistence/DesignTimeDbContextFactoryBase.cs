using BusinessInfo.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BusinessInfo.Persistence
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> :
        IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            var solutionPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..");

            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "BusinessInfo.Persistence.Migration"));

            if (!Directory.Exists(basePath))
            {
                throw new DirectoryNotFoundException($"O caminho {basePath} não foi encontrado. Verifique a estrutura do projeto.");
            }
            Console.WriteLine(basePath);
            Configuration.Build(basePath);
            return Create(connectionString: Configuration.ConnectionString);

        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string is null or empty.", nameof(connectionString));
            }

            Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return CreateNewInstance(optionsBuilder.Options);
        }
    }
}
