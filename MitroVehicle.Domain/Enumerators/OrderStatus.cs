using System.ComponentModel;

namespace MitroVehicle.Domain.Enumerators
{
    public enum OrderStatus
    {
        [Description("Pendente")]
        Pending = 0,

        [Description("Pago")]
        Paid = 1,

        [Description("Atrasado")]
        Late = 2

    }
}
