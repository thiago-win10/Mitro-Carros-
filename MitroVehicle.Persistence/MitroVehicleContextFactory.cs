using Microsoft.EntityFrameworkCore;
using MitroVehicle.Persistence.Context;

namespace MitroVehicle.Persistence
{
    public class MitroVehicleContextFactory : DesignTimeDbContextFactoryBase<MitroVehicleContext>
    {
        protected override MitroVehicleContext CreateNewInstance(DbContextOptions<MitroVehicleContext> options)
        {
            return new MitroVehicleContext(options);
        }
    }
}
