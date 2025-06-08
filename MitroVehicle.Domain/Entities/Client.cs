namespace MitroVehicle.Domain.Entities
{
    public class Client : BaseEntity
    {
        public Guid UserId { get; set; }
        public List<Location> Locations { get; set; } = new List<Location>();
        public User User { get; set; }
        public bool IsAdmin { get; set; }

    }
}
