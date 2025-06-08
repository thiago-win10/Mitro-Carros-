using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MitroVehicle.Application.Users.Command.Update
{
    public class UpdateUserCommandRequest : IRequest<UpdateUserCommandResponse>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
