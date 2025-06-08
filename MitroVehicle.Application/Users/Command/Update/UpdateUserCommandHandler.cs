using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MitroVehicle.Application.Common.AES;
using MitroVehicle.Application.Common.Exceptions;
using MitroVehicle.Application.Common.Interfaces;
using MitroVehicle.Application.Users.Command.Register;
using MitroVehicle.Common;
using MitroVehicle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MitroVehicle.Application.Users.Command.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdateUserCommandResponse>
    {
        private readonly IMitroVechicleContext _context;
        private readonly AesEncryptionService _aesEncryptionService;
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        
        public UpdateUserCommandHandler(IMitroVechicleContext context, ILogger<UpdateUserCommandHandler> logger, AesEncryptionService encryptionService)
        {
            _context = context;
            _aesEncryptionService = encryptionService;
            _logger = logger;
        }
        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Stating update User {data}", request.ToJson());

                Guid userId = request.UserId;
                await UpdateUserEntity(request, userId);
                await _context.SaveChangesAsync(cancellationToken);

                return new UpdateUserCommandResponse { Message = "Usuario atualizado com Suceeso." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private async Task UpdateUserEntity(UpdateUserCommandRequest request, Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user  == null)
            {
                throw new BadRequestException("Usuário não encontrado.");
            }

            user.Age = request.Age;
            user.Phone = _aesEncryptionService.Encrypt(request.Phone.Trim());
            user.Email = _aesEncryptionService.Encrypt(request.Email.Trim());
            user.Password = _aesEncryptionService.Encrypt(request.Password.Trim());
        }

    }
}
