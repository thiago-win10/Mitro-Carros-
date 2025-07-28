using BusinessInfo.Application.Common.AES;
using BusinessInfo.Application.Common.Interfaces;
using BusinessInfo.Application.Common.Models.Response;
using BusinessInfo.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessInfo.Application.Issuer.Command.Create
{
    public class CreateIssuerCommandHandler : IRequestHandler<CreateIssuerCommandRequest, ResponseApiBase<Guid>>
    {
        private readonly IBusinessInfoContext _context;
        private readonly ILogger<CreateIssuerCommandHandler> _logger;
        private readonly AesEncryptionService _aesEncryptionService;


        public CreateIssuerCommandHandler(IBusinessInfoContext context, ILogger<CreateIssuerCommandHandler> logger, AesEncryptionService aesEncryptionService)
        {
            _context = context;
            _logger = logger;
            _aesEncryptionService = aesEncryptionService;
        }

        public async Task<ResponseApiBase<Guid>> Handle(CreateIssuerCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new ResponseApiBase<Guid>();

                _logger.LogInformation("Stating create Issuer {data}", request.ToJson());

                var issuer = await IssuerCreate(request, cancellationToken);
                response.AddSuccess(issuer.Id);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        private async Task<Domain.Entities.Issuer> IssuerCreate(CreateIssuerCommandRequest request, CancellationToken cancellationToken)
        {
            var companyExists = await _context.Companies
                .AnyAsync(c => c.Id == request.CompanyId, cancellationToken);

            if (!companyExists)
                throw new Exception("Empresa (Company) não encontrada.");

            var issuer = new Domain.Entities.Issuer
            {
                Id = Guid.NewGuid(),
                CompanyId = request.CompanyId,
            };

            await _context.Issuers.AddAsync(issuer);
            await _context.SaveChangesAsync(cancellationToken);

            return issuer;
        }
    }
}


