//using MediatR;
//using MitroVehicle.Application.Common.Models.Response;
//using MitroVehicle.Domain.Entities;
//using MitroVehicle.Domain.Enumerators;
//using System.Text.Json.Serialization;

//namespace MitroVehicle.Application.VehicleSaved.Command.Create
//{
//    public class CreateVehicleCommandRequest : IRequest<ResponseApiBase<Guid>>
//    {
//        [JsonIgnore]
//        public Guid? ClientId { get; set; }
//        public string LicensePlate { get; set; }
//        public int Year { get; set; }
//        public string NameVehicle { get; set; }
//        public string UF { get; set; }
//        public string Renavam { get; set; }
//        public TypeVechicle TypeVechicle { get; set; }
//        public string Color { get; set; }

//    }
//}
