using BusinessInfo.Domain.Enumerators;

namespace BusinessInfo.Domain.Entities
{
    public class Vehicle : BaseEntity
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
        public Guid IssuerId { get; set; }
        public Issuer Issuer { get; set; }

    }
}
