using MediatR;
using MitroVehicle.Application.Common.Models.Response;

namespace MitroVehicle.Application.Users.Command.Register.Login
{
    public class LoginCommandRequest : IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginCommandRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
