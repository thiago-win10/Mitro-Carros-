using BusinessInfo.Persistence;
using BusinessInfo.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MitroVehicle.Persistence
{
    public class BusinessInfoContextFactory : DesignTimeDbContextFactoryBase<BusinessInfoContext>
    {
        protected override BusinessInfoContext CreateNewInstance(DbContextOptions<BusinessInfoContext> options)
        {
            return new BusinessInfoContext(options);
        }
    }
}
