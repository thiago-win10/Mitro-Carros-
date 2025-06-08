using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MitroVehicle.Application.Common.Models.Response;
using MitroVehicle.Application.VehicleSaved.Command.Create;
using MitroVehicle.Application.VehicleSaved.Queries.List;

namespace MitroVehicle.Internal.API.Controllers
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

        [HttpGet("list")]
        [ProducesResponseType(typeof(ResponseApiBase<ListVehicleQueryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApiBase<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListVehicles() =>
            Ok(await _mediator.Send(new ListVehicleQueryRequest()));


        [Authorize(Roles = "admin")]
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
