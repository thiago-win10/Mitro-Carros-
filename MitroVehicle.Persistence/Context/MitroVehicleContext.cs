using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MitroVehicle.Application.Common.Interfaces;
using MitroVehicle.Domain.Entities;

namespace MitroVehicle.Persistence.Context
{
    public class MitroVehicleContext : DbContext, IMitroVechicleContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PaymentOrder> PaymentOrders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public MitroVehicleContext() { }
        public MitroVehicleContext(DbContextOptions<MitroVehicleContext> options) : base(options) { }

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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MitroVehicleContext).Assembly);
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
