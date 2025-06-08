using MitroVehicle.Domain.Enumerators;

namespace MitroVehicle.Domain.Entities
{
    public class PaymentOrder : BaseEntity
    {
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime DataPayment { get; set; }

    }
}
