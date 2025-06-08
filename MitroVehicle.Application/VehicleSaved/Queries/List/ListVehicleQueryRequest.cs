using MediatR;
using MitroVehicle.Application.Common.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MitroVehicle.Application.VehicleSaved.Queries.List
{
    public class ListVehicleQueryRequest : IRequest<ResponseApiBase<List<ListVehicleQueryResponse>>>
    {
    }
}
