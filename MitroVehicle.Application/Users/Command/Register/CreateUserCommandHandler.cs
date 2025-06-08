using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MitroVehicle.Application.Common.AES;
using MitroVehicle.Application.Common.Exceptions;
using MitroVehicle.Application.Common.Interfaces;
using MitroVehicle.Application.Common.Models.Response;
using MitroVehicle.Common;
using MitroVehicle.Domain.Entities;
using System.Reflection.Metadata.Ecma335;

namespace MitroVehicle.Application.Users.Command.Register
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, ResponseApiBase<Guid>>
    {
        private readonly IMitroVechicleContext _context;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly AesEncryptionService _aesEncryptionService;


        public CreateUserCommandHandler(IMitroVechicleContext context, ILogger<CreateUserCommandHandler> logger, AesEncryptionService aesEncryptionService)
        {
            _context = context;
            _logger = logger;
            _aesEncryptionService = aesEncryptionService;
        }
        public async Task<ResponseApiBase<Guid>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new ResponseApiBase<Guid>();

                _logger.LogInformation("Stating create User {data}", request.ToJson());

                var user = await UserEntity(request, cancellationToken);
                response.AddSuccess(user.Id);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }

        private async Task<User> UserEntity(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var cripto = _aesEncryptionService.Encrypt(request.Email);
            var existingUser = await _context.Users.AnyAsync(u => u.Email ==  cripto, cancellationToken);
            if (existingUser)
                throw new BadRequestException("Já existe um usuário com este email.");

            Client client = null;

            if (request.Role == "client")
            {
                client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == request.ClientId, cancellationToken);
                if (client == null && request.Role == "client")
                {
                    client = new Client();
                    await _context.Clients.AddAsync(client);
                }
            }
            
            var user = new User
            {
                ClientId = client != null ? client.Id : (Guid?)null,
                Name = request.Name.Trim(),
                Document = _aesEncryptionService.Encrypt(request.Document.Trim().UnMask()),
                Phone = _aesEncryptionService.Encrypt(request.Phone.Trim().UnMask()),
                Age = request.Age,
                Email = _aesEncryptionService.Encrypt(request.Email.Trim()),
                Password = _aesEncryptionService.Encrypt(request.Password.Trim()),
                Role = request.Role
            };
            await _context.Users.AddAsync(user);
            
            if (client != null)
            {
                client.UserId = user.Id;
                _context.Clients.Update(client);
            }
            await _context.SaveChangesAsync(cancellationToken);

            return user;
        }
    }

}