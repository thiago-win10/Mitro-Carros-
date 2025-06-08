using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MitroVehicle.Application.Common.AES;
using MitroVehicle.Application.Common.Interfaces;
using MitroVehicle.Application.Common.Models.Response;
using MitroVehicle.Application.VehicleSaved.Queries.List;
using MitroVehicle.Common;
using MitroVehicle.Domain.Entities;
using MitroVehicle.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MitroVehicle.Application.Location.Queries.List
{
    public class ListLocationQueryHandler : IRequestHandler<ListLocationQueryRequest, ResponseApiBase<List<ListLocationQueryResponse>>>
    {
        private readonly IMitroVechicleContext _context;
        private readonly ILogger<ListVehicleQueryHandler> _logger;
        private readonly AesEncryptionService _aesEncryptionService;
        public ListLocationQueryHandler(IMitroVechicleContext context, ILogger<ListVehicleQueryHandler> logger, AesEncryptionService aesEncryptionService)
        {
            _context = context;
            _logger = logger;
            _aesEncryptionService = aesEncryptionService;
        }

        public async Task<ResponseApiBase<List<ListLocationQueryResponse>>> Handle(ListLocationQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Stating List Vehicle Location {data}", request.ToJson());

                var query = _context.Locations
                            .Include(c => c.Client.User)
                            .Include(x => x.Vehicle)
                            .OrderByDescending(x => x.CreatedAt).AsQueryable();

                List<ListLocationQueryResponse> listLocation = new();
                foreach (var location in query)
                {
                    listLocation.Add(new ListLocationQueryResponse
                    {
                        NameClient = location.Client.User.Name,
                        Dcoument = _aesEncryptionService.Decrypt(location.Client.User.Document),
                        Age = location.Client.User.Age,
                        Phone = _aesEncryptionService.Decrypt(location.Client.User.Phone),
                        Email = _aesEncryptionService.Decrypt(location.Client.User.Email),
                        NameVehicle = location.Vehicle.NameVehicle,
                        LicensePlate = _aesEncryptionService.Decrypt(location.Vehicle.LicensePlate),
                        Year = location.Vehicle.Year,
                        UF = location.Vehicle.UF,
                        Color = location.Vehicle.Color,
                        Renavam = _aesEncryptionService.Decrypt(location.Vehicle.Renavam),
                        TypeVehicle = $"Tipo: {location.Vehicle.TypeVechicle.GetDescription()}",
                        LocationVehicleStatus = $"Status da Locação: {location.Status.GetDescription()}"

                    });
                }

                return new ResponseApiBase<List<ListLocationQueryResponse>>
                {
                    Data = listLocation

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
