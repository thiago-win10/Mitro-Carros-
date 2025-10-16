using BusinessInfo.Application.Common.Models.Response;
using BusinessInfo.Application.Company.Command.Create;
using BusinessInfo.Application.Issuer.Command.Create;
using BusinessInfo.Application.Issuer.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessInfo.Internal.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class IssuersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IssuersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria uma nova empresa (Company).
        /// </summary>
        /// <param name="command">Dados para criação da empresa.</param>
        /// <returns>Retorna o ID da empresa criada.</returns>


        [HttpPost("issuer")]
        [ProducesResponseType(typeof(ResponseApiBase<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApiBase<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterIssuer([FromBody] CreateIssuerCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("list/issuers")]
        [ProducesResponseType(typeof(ResponseApiBase<ListIssuerQueryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApiBase<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetListIssuer() =>
            Ok(await _mediator.Send(new ListIssuerQueryRequest()));

    }
}
