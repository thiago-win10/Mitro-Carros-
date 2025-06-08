using System.ComponentModel;

namespace MitroVehicle.Domain.Enumerators
{
    public enum LocationStatus
    {
        [Description("Ativa")]
        Active = 0,

        [Description("Finalizado")]
        Finish = 1,

        [Description("Cancelado")]
        Canceled = 2,
        
        [Description("Ativa´(Mas prazo de entrega Excedido)")]
        ActiveDeliveryDeadlineExceeded = 3

    }
}
