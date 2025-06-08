using MediatR;
using MitroVehicle.Application.Common.Models.Response;
using MitroVehicle.Domain.Entities;
using MitroVehicle.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MitroVehicle.Application.Location.Command.Register
{
    public class RegisterLocationCommandRequest : IRequest<RegisterLocationCommandResponse>
    {
        [JsonIgnore]
        public Guid ClientId { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime? DataStart { get; set; }
        public DateTime? DataEnd { get; set; }
        
        [JsonIgnore]
        public decimal ValueTotal { get; set; }

    }
}
