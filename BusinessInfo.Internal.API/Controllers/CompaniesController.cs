using BusinessInfo.Application.Common.Models.Response;
using BusinessInfo.Application.Company.Command.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BusinessInfo.Internal.API.Controllers
{

    [ApiController]
    [Route("api/v1/")]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria uma nova empresa (Company).
        /// </summary>
        /// <param name="command">Dados para criação da empresa.</param>
        /// <returns>Retorna o ID da empresa criada.</returns>

        
        [HttpPost("company")]
        [ProducesResponseType(typeof(ResponseApiBase<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApiBase<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterCompany([FromBody] CreateCompanyCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
