using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MitroVehicle.Application.Common.AES;
using MitroVehicle.Application.Common.Interfaces;
using MitroVehicle.Application.VehicleSaved.Command.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MitroVehicle.Application.StatusLocationUpdateService
{
    public class StatusLocationUpdateHandler : IRequestHandler<StatusLocationUpdateRequest, StatusLocationUpdateResponse>
    {
        private readonly IMitroVechicleContext _context;
        private readonly ILogger<StatusLocationUpdateHandler> _logger;
        public StatusLocationUpdateHandler(IMitroVechicleContext context, ILogger<StatusLocationUpdateHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<StatusLocationUpdateResponse> Handle(StatusLocationUpdateRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando atualização de status das Locações.");

            var locations = await _context.Locations
                                  .Include(x => x.PaymentOrders)
                                  .Where(x => x.Status != Domain.Enumerators.LocationStatus.Finish)
                                  .ToListAsync();

            foreach (var location in locations)
            {
                if (location.PaymentOrders != null && location.PaymentOrders.Any(x=>x.Status == Domain.Enumerators.OrderStatus.Paid))
                {
                    if (location.Status != Domain.Enumerators.LocationStatus.Finish)
                    {
                        _logger.LogInformation($"Atualizando Locação {location.Id} para Finalizada");
                        location.Status = Domain.Enumerators.LocationStatus.Finish;
                        _context.SetModifiedState(location);
                    }
                }
                else
                {
                    if (location.Status != Domain.Enumerators.LocationStatus.ActiveDeliveryDeadlineExceeded)
                    {
                        _logger.LogInformation($"Atualizando Locação {location.Id} para Atrasada");
                        location.Status = Domain.Enumerators.LocationStatus.ActiveDeliveryDeadlineExceeded;
                        _context.SetModifiedState(location);
                    }
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Atualização de status das Locações concluídas");
            return new StatusLocationUpdateResponse() { Success = true };   

        }
    }
}
