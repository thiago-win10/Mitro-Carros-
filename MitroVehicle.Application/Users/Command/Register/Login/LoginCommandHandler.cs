using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MitroVehicle.Application.Common.AES;
using MitroVehicle.Application.Common.Exceptions;
using MitroVehicle.Application.Common.Interfaces;
using MitroVehicle.Application.Common.Jwt;

namespace MitroVehicle.Application.Users.Command.Register.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginResponse>
    {
        private readonly IMitroVechicleContext _context;
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly AuthService _authService;
        private readonly AesEncryptionService _encryptionService;

        public LoginCommandHandler(IMitroVechicleContext context, AuthService authService, AesEncryptionService aes, ILogger<LoginCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _authService = authService;
            _encryptionService = aes;
        }

        public async Task<LoginResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var cripto = _encryptionService.Encrypt(request.Email);
            var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Email == cripto);

            if (user == null)
            {
                throw new BadRequestException("Usuário não exise");
            }

            var decrypt = _encryptionService.Decrypt(user.Password);
            if (decrypt != request.Password)
            {
                throw new BadRequestException("Usuário ou senha inválidos");

            }

            var token = _authService.GenerateToken(user);

            return new LoginResponse { Token = token };
        }
    }
}
