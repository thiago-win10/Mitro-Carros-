using MediatR;
using MitroVehicle.Application.Common.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MitroVehicle.Application.Location.Queries.List
{
    public class ListLocationQueryRequest : IRequest<ResponseApiBase<List<ListLocationQueryResponse>>>
    {
    }
}
