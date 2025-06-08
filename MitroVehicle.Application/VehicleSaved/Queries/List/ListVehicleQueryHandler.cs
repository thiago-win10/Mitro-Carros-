using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MitroVehicle.Application.Common.AES;
using MitroVehicle.Application.Common.Interfaces;
using MitroVehicle.Application.Common.Models.Response;
using MitroVehicle.Application.VehicleSaved.Command.Create;
using MitroVehicle.Common;
using MitroVehicle.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MitroVehicle.Application.VehicleSaved.Queries.List
{
    public class ListVehicleQueryHandler : IRequestHandler<ListVehicleQueryRequest, ResponseApiBase<List<ListVehicleQueryResponse>>>
    {
        private readonly IMitroVechicleContext _context;
        private readonly ILogger<ListVehicleQueryHandler> _logger;
        private readonly AesEncryptionService _aesEncryptionService;
        private readonly IRedisCaching _redis;
        public ListVehicleQueryHandler(IRedisCaching redis, AesEncryptionService aesEncryptionService, IMitroVechicleContext context, ILogger<ListVehicleQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
            _aesEncryptionService = aesEncryptionService;
            _redis = redis;
        }

        public async Task<ResponseApiBase<List<ListVehicleQueryResponse>>> Handle(ListVehicleQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Stating List Vehicle {data}", request.ToJson());

                var cacheKey = $"List Vehicle: {request.ToJson()}";
                var cacheData = await _redis.GetAsync<List<ListVehicleQueryResponse>>(cacheKey);
                if (cacheData is not null)
                {
                    _logger.LogInformation("Returning vehicles from cache.");
                    return new ResponseApiBase<List<ListVehicleQueryResponse>>
                    {
                        Data = cacheData
                    };
                }

                var query = await _context.Vehicles
                            .Include(x => x.Locations)
                            .OrderByDescending(x => x.CreatedAt).ToListAsync();

                List<ListVehicleQueryResponse> listResponse = new();
                foreach (var vehicle in query)
                {
                    listResponse.Add(new ListVehicleQueryResponse
                    {
                        LicensePlate = _aesEncryptionService.Decrypt(vehicle.LicensePlate),
                        NameVehicle = vehicle.NameVehicle,
                        Renavam = _aesEncryptionService.Decrypt(vehicle.Renavam),
                        Year = vehicle.Year,
                        UF = vehicle.UF,
                        Color = vehicle.Color,
                        NameTypeVehicle = $"Tipo: {vehicle.TypeVechicle.GetDescription()}",
                        StatusVehicle = vehicle.Locations.Any(l => l.Status == LocationStatus.Active) ? "Alugado" : "Disponivel para Locação"

                    });
                }

                await _redis.SetAsync(cacheKey, listResponse, TimeSpan.FromMinutes(10));
                return new ResponseApiBase<List<ListVehicleQueryResponse>>
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
