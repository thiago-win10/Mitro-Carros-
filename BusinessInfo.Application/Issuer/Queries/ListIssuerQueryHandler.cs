using BusinessInfo.Application.Common.AES;
using BusinessInfo.Application.Common.Interfaces;
using BusinessInfo.Application.Common.Models.Response;
using BusinessInfo.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessInfo.Application.Issuer.Queries
{
    public class ListIssuerQueryHandler : IRequestHandler<ListIssuerQueryRequest, ResponseApiBase<List<ListIssuerQueryResponse>>>
    {
        private readonly IBusinessInfoContext _context;
        private readonly ILogger<ListIssuerQueryHandler> _logger;
        private readonly AesEncryptionService _aesEncryptionService;
        public ListIssuerQueryHandler(AesEncryptionService aesEncryptionService, IBusinessInfoContext context, ILogger<ListIssuerQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
            _aesEncryptionService = aesEncryptionService;
        }

        public async Task<ResponseApiBase<List<ListIssuerQueryResponse>>> Handle(ListIssuerQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Stating List Issuer {data}", request.ToJson());

                var query = await _context.Issuers
                            .Include(x => x.Companies)
                            .OrderByDescending(x => x.CreatedAt).ToListAsync();

                List<ListIssuerQueryResponse> listResponse = new();
                foreach (var issuer in query)
                {
                    listResponse.Add(new ListIssuerQueryResponse
                    {
                        IssuerId = issuer.Id,
                        NameIssuer = issuer.Companies.Name,
                        CreatedAt = issuer.CreatedAt
                    });
                }

                return new ResponseApiBase<List<ListIssuerQueryResponse>>
                {
                    Data = listResponse
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }
}
