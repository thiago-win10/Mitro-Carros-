namespace MitroVehicle.Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid? ClientId { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Client Client { get; set; }

    }
}
