using MitroVehicle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MitroVehicle.Application.Users.Queries
{
    public class ListUsersQueryResponse
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
