namespace BusinessInfo.Domain.Entities
{
    public class Issuer : BaseEntity
    {
        public Guid CompanyId { get; set; }
        public Company Companies { get; set; }
        public List<Vehicle> Vehicles { get; set; }

    }
}
