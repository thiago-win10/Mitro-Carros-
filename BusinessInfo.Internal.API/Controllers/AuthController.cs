using BusinessInfo.Application.Common.Models.Response;
using BusinessInfo.Application.Login.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BusinessInfo.Internal.API.Controllers
{

    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ResponseApiBase<LoginCommandResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApiBase<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
