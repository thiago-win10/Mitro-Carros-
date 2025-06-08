using MitroVehicle.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MitroVehicle.Application.Location.Queries.List
{
    public class ListLocationQueryResponse
    {
        public string NameClient { get; set; }
        public string Dcoument { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string NameVehicle { get; set; }
        public string LicensePlate { get; set; }
        public int Year { get; set; }
        public string UF { get; set; }
        public string Color { get; set; }
        public string Renavam { get; set; }
        public TypeVechicle Status { get; set; }
        public string TypeVehicle { get; set; }
        public LocationStatus StatusLocation { get; set; }
        public string LocationVehicleStatus { get; set; }
    }
}
