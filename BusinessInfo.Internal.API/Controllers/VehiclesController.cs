using BusinessInfo.Application.Common.Models.Response;
using BusinessInfo.Application.Vehicle.Command.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessInfo.Application.VehicleSaved.Queries.List;
using System.Net;

namespace BusinessInfo.Internal.API.Controllers
{
    [Route("api/v1/vehicles")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseApiBase<PaginatedModelResponse<ListVehicleQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApiBase<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListVehicles([FromQuery] ListVehicleQueryRequest request)
        {
            var responseApi = new ResponseApiBase<PaginatedModelResponse<ListVehicleQueryResponse>>();
            request = new ListVehicleQueryRequest
            {
                IssuerName = request.IssuerName,
                ModelCar = request.ModelCar,
                DailyRate = request.DailyRate,
                Plate = request.Plate,
                NameVehicle = request.NameVehicle,
                CollorCar = request.CollorCar,
                YearCar = request.YearCar,
                Brand = request.Brand,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            };

            var response = await _mediator.Send(request);
            responseApi.AddSuccess(new PaginatedModelResponse<ListVehicleQueryResponse>(response.Data.Total, response.Data.Items));
            return StatusCode(HttpStatusCode.OK.GetHashCode(), responseApi);
        }
            

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseApiBase<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApiBase<string>), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateVehicleAsync([FromBody] CreateVehicleCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
