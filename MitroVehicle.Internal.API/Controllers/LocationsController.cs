using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MitroVehicle.Application.Common.Models.Response;
using MitroVehicle.Application.Location.Command.Register;
using MitroVehicle.Application.Location.Queries.List;
using MitroVehicle.Application.VehicleSaved.Command.Create;
using MitroVehicle.Application.VehicleSaved.Queries.List;

namespace MitroVehicle.Internal.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "admin")]
        [HttpGet("list-locations-vehicles")]
        [ProducesResponseType(typeof(ResponseApiBase<List<ListLocationQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApiBase<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListVehiclesLocations() =>
            Ok(await _mediator.Send(new ListLocationQueryRequest()));


        [Authorize(Roles = "client")]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseApiBase<RegisterLocationCommandResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApiBase<string>), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateVehicleLocationAsync([FromBody] RegisterLocationCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
