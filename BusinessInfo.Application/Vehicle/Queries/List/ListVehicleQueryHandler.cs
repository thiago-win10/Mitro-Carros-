using BusinessInfo.Application.Common.AES;
using BusinessInfo.Application.Common.Interfaces;
using BusinessInfo.Application.Common.Models.Response;
using BusinessInfo.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessInfo.Application.VehicleSaved.Queries.List
{
    public class ListVehicleQueryHandler : IRequestHandler<ListVehicleQueryRequest, ResponseApiBase<PaginatedModelResponse<ListVehicleQueryResponse>>>
    {
        private readonly IBusinessInfoContext _context;
        private readonly ILogger<ListVehicleQueryHandler> _logger;
        private readonly IRedisCaching _redis;
        public ListVehicleQueryHandler(IRedisCaching redis, IBusinessInfoContext context, ILogger<ListVehicleQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
            _redis = redis;
        }

        public async Task<ResponseApiBase<PaginatedModelResponse<ListVehicleQueryResponse>>> Handle(ListVehicleQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting List Vehicle {data}", request.ToJson());

                var cacheKey = $"List Vehicle: {request.ToJson()}";
                var cacheData = await _redis.GetAsync<PaginatedModelResponse<ListVehicleQueryResponse>>(cacheKey);

                if (cacheData is not null)
                {
                    _logger.LogInformation("Returning vehicles from cache.");
                    return new ResponseApiBase<PaginatedModelResponse<ListVehicleQueryResponse>>
                    {
                        Data = cacheData
                    };
                }

                var query = _context.Vehicles.OrderByDescending(x => x.CreatedAt).AsQueryable();
                if (!string.IsNullOrWhiteSpace(request.Plate))
                {
                    var encryptedPlate = request.Plate.Trim();
                    query = query.Where(x => x.Plate == encryptedPlate);
                }

                if (!string.IsNullOrWhiteSpace(request.NameVehicle))
                    query = query.Where(x => x.NameVehicle.Contains(request.NameVehicle.Trim()));

                if (!string.IsNullOrWhiteSpace(request.ModelCar))
                    query = query.Where(x => x.Model.Contains(request.ModelCar.Trim()));

                if (!string.IsNullOrWhiteSpace(request.Brand))
                    query = query.Where(x => x.Brand.Contains(request.Brand.Trim()));

                if (!string.IsNullOrWhiteSpace(request.CollorCar))
                    query = query.Where(x => x.Collor.Contains(request.CollorCar.Trim()));

                int pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
                int pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

                var totalItems = await query.CountAsync(cancellationToken);
                var vehicles = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

                List<ListVehicleQueryResponse> listResponse = new();

                foreach (var vehicle in vehicles)
                {
                    listResponse.Add(new ListVehicleQueryResponse
                    {
                        Plate = vehicle.Plate,
                        NameVehicle = vehicle.NameVehicle,
                        Renavam = vehicle.Renavam,
                        Year = vehicle.Year,
                        Color = vehicle.Collor,
                        NameTypeVehicle = vehicle.NameVehicle,
                        TypeVechicle = vehicle.TypeVechicle,
                        ModelCar = vehicle.Model,
                        Brand = vehicle.Brand,
                        StatusVehicle = null  //vehicle.Locations.Any(l => l.Status == LocationStatus.Active)
                            //? "Alugado"
                            //: "Disponível para Locação"
                    });
                }

                var paginatedResult = new PaginatedModelResponse<ListVehicleQueryResponse>(totalItems, listResponse);

                await _redis.SetAsync(cacheKey, paginatedResult, TimeSpan.FromMinutes(10));

                return new ResponseApiBase<PaginatedModelResponse<ListVehicleQueryResponse>>
                {
                    Data = paginatedResult
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
