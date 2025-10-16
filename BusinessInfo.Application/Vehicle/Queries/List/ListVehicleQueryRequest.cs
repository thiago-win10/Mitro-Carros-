using BusinessInfo.Application.Common.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessInfo.Application.VehicleSaved.Queries.List
{
    public class ListVehicleQueryRequest : IRequest<ResponseApiBase<PaginatedModelResponse<ListVehicleQueryResponse>>>
    {
        public string Plate {  get; set; }
        public string NameVehicle { get; set; }
        public decimal DailyRate { get; set; }
        public string IssuerName { get; set; }
        public string ModelCar { get; set; }
        public string CollorCar { get; set; }
        public string Brand { get; set; }
        public int YearCar { get; set; }
        public int PageNumber {  get; set; }
        public int PageSize { get; set; }



    }
}
