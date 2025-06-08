using MediatR;
using MitroVehicle.Application.Common.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MitroVehicle.Application.Users.Queries
{
    public class ListUsersQueryRequest : IRequest<ResponseApiBase<List<ListUsersQueryResponse>>>
    {
        public ListUsersQueryRequest(Guid id)
        {
            UserId = id;
        }
        public Guid UserId { get; set; }
    }
}
