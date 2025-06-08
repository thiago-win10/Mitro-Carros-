using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MitroVehicle.Application.Common.AES;
using MitroVehicle.Application.Common.Interfaces;
using MitroVehicle.Application.Common.Models.Response;
using MitroVehicle.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MitroVehicle.Application.Users.Queries
{
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQueryRequest, ResponseApiBase<List<ListUsersQueryResponse>>>
    {
        private readonly IMitroVechicleContext _context;
        private readonly ILogger<ListUsersQueryHandler> _logger;
        private readonly AesEncryptionService _aesEncryptionService;
        public ListUsersQueryHandler(IMitroVechicleContext context, AesEncryptionService aesEncryptionService, ILogger<ListUsersQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
            _aesEncryptionService = aesEncryptionService;
        }

        public async Task<ResponseApiBase<List<ListUsersQueryResponse>>> Handle(ListUsersQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Stating List The User {data}", request.ToJson());
                var query = _context.Users
                            .AsNoTrackingWithIdentityResolution()
                            .Include(x => x.Client)
                            .Where(x => x.Client.UserId.ToString() == request.UserId.ToString()).AsQueryable();

                query = query.OrderByDescending(x => x.CreatedAt);

                var users = await query.Select(x => new ListUsersQueryResponse
                {
                    UserId = x.Id.ToString(),
                    Name = x.Name,
                    Document = _aesEncryptionService.Decrypt(x.Document),
                    Age = x.Age,
                    Email = _aesEncryptionService.Decrypt(x.Email),
                    Phone = _aesEncryptionService.Decrypt(x.Phone),
                 }).ToListAsync(cancellationToken);

                return new ResponseApiBase<List<ListUsersQueryResponse>>
                {
                    Data = users
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro in {nameof(ListUsersQueryResponse)}.");
                throw;
            }
        }
    }
}
