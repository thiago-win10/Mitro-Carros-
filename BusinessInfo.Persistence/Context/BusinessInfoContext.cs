using BusinessInfo.Application.Common.Interfaces;
using BusinessInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace BusinessInfo.Persistence.Context
{
    public class BusinessInfoContext : DbContext, IBusinessInfoContext
    {
        public DbSet<Issuer> Issuers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public BusinessInfoContext() { }
        public BusinessInfoContext(DbContextOptions<BusinessInfoContext> options) : base(options) { }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.Entity.CreatedAt == DateTime.MinValue)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.State = EntityState.Added;
                }
                else if (entry.State == EntityState.Modified)
                    entry.Entity.ModifiedAt = DateTime.UtcNow;

            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return Database.CreateExecutionStrategy();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BusinessInfoContext).Assembly);
        }

        public void SetModifiedState<T>(T entity)
        {
            base.Entry(entity).State = EntityState.Modified;
        }

        public void AttachModelToContext<T>(T entity)
        {
            base.Attach(entity);
        }

        public DatabaseFacade DataBaseOrigin { get { return this.Database; } }
    }
}
