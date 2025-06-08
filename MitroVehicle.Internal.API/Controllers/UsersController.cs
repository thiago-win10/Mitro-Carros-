using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MitroVehicle.Application.Common.Models.Response;
using MitroVehicle.Application.Users.Command.Register;
using MitroVehicle.Application.Users.Command.Update;
using MitroVehicle.Application.Users.Queries;
using MitroVehicle.Internal.API.Controllers;

namespace MitroVehicle.Internal.API.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseApiBase<ListUsersQueryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApiBase<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseApiBase<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser([FromRoute] Guid id) =>
            Ok(await _mediator.Send(new ListUsersQueryRequest(id)));

        //[HttpGet]
        //[ProducesResponseType(typeof(ResponseApiBase<ListUsersQueryResponse>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ResponseApiBase<string>), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ResponseApiBase<string>), StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> ListUsers([FromRoute] ListUsersQueryRequest request)
        //{
        //    var response = await _mediator.Send(request);
        //    return Ok(response);
        //} //Criar um get list geral

        [HttpPost]
        [ProducesResponseType(typeof(ResponseApiBase<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApiBase<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseApiBase<UpdateUserCommandResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApiBase<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserCommandRequest request)
        {
            request.UserId = id;
            var response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}

