using System.ComponentModel;

namespace BusinessInfo.Domain.Enumerators
{
    public enum TypeVechicle
    {
        [Description("Carro")]
        Automobile = 0,

        [Description("Motocicleta")]
        Motorcycle = 1,

        [Description("Onibus")]
        Bus = 2,

    }
}
