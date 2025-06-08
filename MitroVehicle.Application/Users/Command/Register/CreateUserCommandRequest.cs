using MediatR;
using MitroVehicle.Application.Common.Models.Response;
using System.Text.Json.Serialization;

namespace MitroVehicle.Application.Users.Command.Register
{
    public class CreateUserCommandRequest : IRequest<ResponseApiBase<Guid>>
    {
        [JsonIgnore]
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
