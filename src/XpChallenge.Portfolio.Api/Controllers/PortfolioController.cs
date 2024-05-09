using MediatR;
using Microsoft.AspNetCore.Mvc;
using XpChallenge.Portfolio.Api.Requests;
using XpChallenge.Portfolio.Application.Commands.CriarPortfolio;

namespace XpChallenge.Portfolio.Api.Controllers
{
    [ApiController]
    public class PortfolioController(IMediator mediator,
        ILogger<PortfolioController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<PortfolioController> _logger = logger;

        [HttpPost]
        public async Task<IActionResult> CriarPortfolio([FromBody] CriarPortfolioRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new CriarPortfolioCommand(request.Nome, request.Descricao), cancellationToken);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult ExcluirPortfolio()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult ObterPortfolio()
        {
            return Ok();
        }
    }
}
