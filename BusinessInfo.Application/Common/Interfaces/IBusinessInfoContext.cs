using BusinessInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace BusinessInfo.Application.Common.Interfaces
{
    public interface IBusinessInfoContext
    {
        public DbSet<Domain.Entities.Issuer> Issuers { get; set; }
        public DbSet<Domain.Entities.Company> Companies { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        IExecutionStrategy CreateExecutionStrategy();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        void SetModifiedState<T>(T entity);
        void AttachModelToContext<T>(T entity);
        DatabaseFacade DataBaseOrigin { get; }
    }
}
