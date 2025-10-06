using BusinessInfo.Application.Common.AES;
using BusinessInfo.Application.Common.Exceptions;
using BusinessInfo.Application.Common.Interfaces;
using BusinessInfo.Application.Common.Models.Response;
using BusinessInfo.Application.Vehicle.Command.Create;
using BusinessInfo.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace BusinessInfo.Application.VehicleSaved.Command.Create
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommandRequest, ResponseApiBase<Guid>>
    {
        private readonly IBusinessInfoContext _context;
        private readonly ILogger<CreateVehicleCommandHandler> _logger;
        private readonly AesEncryptionService _aesEncryptionService;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public CreateVehicleCommandHandler(IHttpContextAccessor contextAccessor, IBusinessInfoContext context, ILogger<CreateVehicleCommandHandler> logger, AesEncryptionService aesEncryptionService)
        {
            _context = context;
            _logger = logger;
            _aesEncryptionService = aesEncryptionService;
            _httpContextAccessor = contextAccessor;
        }

        public async Task<ResponseApiBase<Guid>> Handle(CreateVehicleCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new ResponseApiBase<Guid>();

                _logger.LogInformation("Stating create Vehicle {data}", request.ToJson());

                var vehicle = await VehicleEntity(request);
                await _context.SaveChangesAsync(cancellationToken);

                response.AddSuccess(vehicle.Id);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }

        private async Task<Domain.Entities.Vehicle> VehicleEntity(CreateVehicleCommandRequest request)
        {

            var existLicensePlate = await _context.Vehicles.FirstOrDefaultAsync(c => c.Plate == request.Plate);

            if (existLicensePlate is not null)
            {
                throw new BadRequestException($"A placa {request.Plate} já existe.");
            }

            var issuer = _httpContextAccessor.HttpContext?.User?.FindFirst("IssuerId")?.Value;


            if (!Guid.TryParse(issuer, out var issuerId))
            {
                throw new UnauthorizedAccessException("IssuerId inválido ou não encontrado no token.");
            }

            _logger.LogInformation("Cliente logado:", issuer);


            var vehicle = new Domain.Entities.Vehicle
            {
                NameVehicle = request.NameVehicle,
                Plate = request.Plate,
                Model = request.Model,
                Brand = request.Brand,
                Year = request.Year,
                Collor = request.Collor,
                Renavam = request.Renavam,
                DailyRate = request.DailyRate,
                TypeVechicle = request.TypeVechicle,
                IssuerId = issuerId
            };
            await _context.Vehicles.AddAsync(vehicle);

            return vehicle;
            
        }

    }
}

