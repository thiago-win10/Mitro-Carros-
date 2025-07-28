//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using MitroVehicle.Application.Common.AES;
//using MitroVehicle.Application.Common.Exceptions;
//using MitroVehicle.Application.Common.Interfaces;
//using MitroVehicle.Application.Common.Models.Response;
//using MitroVehicle.Application.Users.Command.Register;
//using MitroVehicle.Common;
//using MitroVehicle.Domain.Entities;
//using MitroVehicle.Domain.Enumerators;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MitroVehicle.Application.VehicleSaved.Command.Create
//{
//    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommandRequest, ResponseApiBase<Guid>>
//    {
//        private readonly IMitroVechicleContext _context;
//        private readonly ILogger<CreateVehicleCommandHandler> _logger;
//        private readonly AesEncryptionService _aesEncryptionService;


//        public CreateVehicleCommandHandler(IMitroVechicleContext context, ILogger<CreateVehicleCommandHandler> logger, AesEncryptionService aesEncryptionService)
//        {
//            _context = context;
//            _logger = logger;
//            _aesEncryptionService = aesEncryptionService;
//        }

//        public async Task<ResponseApiBase<Guid>> Handle(CreateVehicleCommandRequest request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var response = new ResponseApiBase<Guid>();

//                _logger.LogInformation("Stating create Vehicle {data}", request.ToJson());

//                var vehicle = await VehicleEntity(request);
//                await _context.SaveChangesAsync(cancellationToken);

//                response.AddSuccess(vehicle.Id);
//                return response;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                throw;
//            }

//        }

//        private async Task<Vehicle> VehicleEntity(CreateVehicleCommandRequest request)
//        {

//            var existLicensePlate = await _context.Vehicles
//                                    .FirstOrDefaultAsync(c => c.LicensePlate == _aesEncryptionService.Encrypt(request.LicensePlate));

//            if (existLicensePlate is not null)
//            {
//                throw new BadRequestException($"A placa {request.LicensePlate} já existe.");
//            }
//            else
//            {
//                var vehicle = new Vehicle(
//                _aesEncryptionService.Encrypt(request.LicensePlate.Trim().UnMask()),
//                request.Year,
//                request.NameVehicle.Trim(),
//                request.UF,
//                _aesEncryptionService.Encrypt(request.Renavam.Trim().UnMask()),
//                request.Color.Trim(),
//                request.TypeVechicle

//            );
//            await _context.Vehicles.AddAsync(vehicle);

//            return vehicle;
//            }


//        }
//    }
//}
