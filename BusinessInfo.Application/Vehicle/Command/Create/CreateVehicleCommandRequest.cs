using BusinessInfo.Application.Common.Models.Response;
using BusinessInfo.Domain.Enumerators;
using MediatR;

namespace BusinessInfo.Application.Vehicle.Command.Create
{
    public class CreateVehicleCommandRequest : IRequest<ResponseApiBase<Guid>>
    {
        public string NameVehicle { get; set; }
        public string Plate { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public string Collor { get; set; }
        public string Renavam { get; set; }
        public decimal DailyRate { get; set; }
        public TypeVechicle TypeVechicle { get; set; }

    }
}
