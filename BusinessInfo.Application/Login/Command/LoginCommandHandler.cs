using BusinessInfo.Application.Common.AES;
using BusinessInfo.Application.Common.Exceptions;
using BusinessInfo.Application.Common.Interfaces;
using BusinessInfo.Application.Common.Jwt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessInfo.Application.Login.Command
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly IBusinessInfoContext _context;
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly AuthService _authService;
        private readonly AesEncryptionService _encryptionService;

        public LoginCommandHandler(IBusinessInfoContext context, AuthService authService, AesEncryptionService aes, ILogger<LoginCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _authService = authService;
            _encryptionService = aes;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var cripto = _encryptionService.Encrypt(request.Email);
            var user = await _context.Issuers.Include(x =>x.Companies).AsNoTracking().SingleOrDefaultAsync(x => x.Companies.ContactPerson.Email == cripto);

            if (user == null)
            {
                throw new BadRequestException("Usuário não exise");
            }

            var decrypt = _encryptionService.Decrypt(user.Companies.ContactPerson.Email);
            if (decrypt != request.Email)
            {
                throw new BadRequestException("Dados inválidos");

            }

            var token = _authService.GenerateToken(user);

            return new LoginCommandResponse { Token = token };
        }

    }
}
