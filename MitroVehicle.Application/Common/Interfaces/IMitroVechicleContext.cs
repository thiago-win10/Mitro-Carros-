using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MitroVehicle.Domain.Entities;

namespace MitroVehicle.Application.Common.Interfaces
{
    public interface IMitroVechicleContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Domain.Entities.Location> Locations { get; set; }
        public DbSet<PaymentOrder> PaymentOrders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        IExecutionStrategy CreateExecutionStrategy();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        void SetModifiedState<T>(T entity);
        void AttachModelToContext<T>(T entity);
        DatabaseFacade DataBaseOrigin { get; }
    }
}
