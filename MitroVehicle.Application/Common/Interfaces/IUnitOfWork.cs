using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MitroVehicle.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        ILocationRepository Locations { get; }
        //IPaymentOrderRepository PaymentOrders { get; }
        //IClientRepository Clients { get; }
        //IVehicleRepository Vehicles { get; }

        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}
