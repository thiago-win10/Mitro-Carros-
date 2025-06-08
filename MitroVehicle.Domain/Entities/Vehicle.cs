using MitroVehicle.Domain.Enumerators;
using System.Text.Json.Serialization;

namespace MitroVehicle.Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        public Guid? ClientId { get; set; }
        public string LicensePlate { get; set; }
        public int Year { get; set; }
        public string NameVehicle { get; set; }
        public string UF { get; set; } 
        public string Renavam { get; set; }
        public string Color { get; set; }
        public TypeVechicle TypeVechicle { get; set; }
        public List<Location> Locations { get; set; } = new List<Location>();

        public Vehicle(string licensePlate, int year, string nameVehicle, string uF, string renavam, string color, TypeVechicle typeVechicle)
        {
            LicensePlate = licensePlate;
            Year = year;
            NameVehicle = nameVehicle;
            UF = uF;
            Renavam = renavam;
            TypeVechicle = typeVechicle;
            Color = color;
        }
    }
}
