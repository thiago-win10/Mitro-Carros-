using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MitroVehicle.Application.Common.Interfaces
{
    public interface ILocationRepository
    {
        Task<List<Domain.Entities.Location>> GetLocationsToProcessAsync();

    }
}
