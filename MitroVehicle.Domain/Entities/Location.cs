using MitroVehicle.Domain.Enumerators;

namespace MitroVehicle.Domain.Entities
{
    public class Location : BaseEntity
    {
        public Guid ClientId { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime? DataStart { get; set; }
        public DateTime? DataEnd { get; set; }
        public decimal ValueTotal { get; set; }
        public LocationStatus Status { get; set; }
        public string LocationVehicleStatus { get; set;  }
        public Client Client { get; set; }
        public Vehicle Vehicle { get; set; }
        public List<PaymentOrder> PaymentOrders { get; set; } = new List<PaymentOrder>();

        public void seStatus(string status)
        {
            Status = status switch
            {
                "Ativa" => LocationStatus.Active,
                "Finalizado" => LocationStatus.Finish,
                "Cancelado" => LocationStatus.Canceled,
                _ => Status
            };
            LocationVehicleStatus = status;
        }
    }
}
