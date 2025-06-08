namespace MitroVehicle.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
