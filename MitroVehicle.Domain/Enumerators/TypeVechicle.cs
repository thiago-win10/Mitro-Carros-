using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MitroVehicle.Domain.Enumerators
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
