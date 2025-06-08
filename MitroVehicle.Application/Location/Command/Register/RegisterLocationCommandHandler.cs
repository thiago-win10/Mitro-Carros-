using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MitroVehicle.Application.Common.AES;
using MitroVehicle.Application.Common.Exceptions;
using MitroVehicle.Application.Common.Interfaces;
using MitroVehicle.Application.Common.Models.Response;
using MitroVehicle.Common;
using MitroVehicle.Domain.Entities;
using MitroVehicle.Domain.Enumerators;

namespace MitroVehicle.Application.Location.Command.Register
{
    public class RegisterLocationCommandHandler : IRequestHandler<RegisterLocationCommandRequest, RegisterLocationCommandResponse>
    {
        private readonly IMitroVechicleContext _context;
        private readonly ILogger<RegisterLocationCommandHandler> _logger;
        private readonly AesEncryptionService _aesEncryptionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        const decimal priceDay = 100.00m;


        public RegisterLocationCommandHandler(IMitroVechicleContext context,IHttpContextAccessor contextAccessor, ILogger<RegisterLocationCommandHandler> logger, AesEncryptionService aesEncryptionService)
        {
            _context = context;
            _logger = logger;
            _aesEncryptionService = aesEncryptionService;
            _httpContextAccessor = contextAccessor;
        }
        public async Task<RegisterLocationCommandResponse> Handle(RegisterLocationCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {

                _logger.LogInformation("Stating create Location the Vehicle {data}", request.ToJson());

                var vehicle = await VehicleLocationData(request);
                await _context.SaveChangesAsync(cancellationToken);

                return new RegisterLocationCommandResponse { Message = "Locação efetuada com Suceeso." };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }

        private async Task<Domain.Entities.Location> VehicleLocationData(RegisterLocationCommandRequest request)
        {

            var client = _httpContextAccessor.HttpContext?.User?.FindFirst("ClientId")?.Value;


            if (!Guid.TryParse(client, out var clienteId))
            {
                throw new UnauthorizedAccessException("ClienteId inválido ou não encontrado no token.");
            }

            _logger.LogInformation("Cliente logado:", request.ClientId = clienteId);

            var vehicle = await _context.Vehicles
                                .Include(v => v.Locations)
                                .FirstOrDefaultAsync(v => v.Id == request.VehicleId);

            if (vehicle == null)
                throw new BadRequestException("Veículo não encontrado.");

            var vehicleLocation = await _context.Locations
                                  .AnyAsync(l => l.VehicleId == request.VehicleId && l.Status == LocationStatus.Active);

            if (vehicleLocation)
                throw new BadRequestException("Veículo já está alugado.");

            int daysLocation = (request.DataEnd.Value.Date - request.DataStart.Value.Date).Days;
            if (daysLocation <= 0)
            {
                throw new BadRequestException("Período inválido de locação.");

            }

            decimal total = priceDay * daysLocation;

            var location = new Domain.Entities.Location
            {
                ClientId = clienteId,
                DataEnd = request.DataEnd,
                VehicleId = vehicle.Id,
                ValueTotal = total,
                Status = LocationStatus.Active,
                LocationVehicleStatus = LocationStatus.Active.ToString()

            };
            await _context.Locations.AddAsync(location);

            var orderPayment = new PaymentOrder
            {
                LocationId = location.Id,
                Status = OrderStatus.Pending,
                Amount = total,
                DataPayment = location.DataEnd.Value

            };

            await _context.PaymentOrders.AddAsync(orderPayment);

            return location;


        }
    }
}
