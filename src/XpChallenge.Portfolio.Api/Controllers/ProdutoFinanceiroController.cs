using MediatR;
using Microsoft.AspNetCore.Mvc;
using XpChallenge.Portfolio.Api.Requests;
using XpChallenge.Portfolio.Application.Commands.AlterarPesoProdutoFinanceiro;
using XpChallenge.Portfolio.Application.Commands.IncluirProdutoFinanceiro;

namespace XpChallenge.Portfolio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoFinanceiroController(IMediator mediator,
        ILogger<ProdutoFinanceiroController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<ProdutoFinanceiroController> _logger = logger;

        [HttpPost]
        public async Task<IActionResult> IncluirProdutoFinanceiro([FromBody] IncluirProdutoFinanceiroRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new IncluirProdutoFinanceiroCommand(request.IdPortfolio, request.Nome, request.IdCategoria, request.Peso), cancellationToken);
            return Ok(result);
        }

        [HttpPut("/peso")]
        public async Task<ActionResult> AlterarPeso([FromBody] AlterarPesoProdutoFinanceiroRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new AlterarPesoProdutoFinanceiroCommand(request.IdPortfolio, request.Nome, request.Peso));
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult RemoverProduto()
        {
            return Ok();
        }
    }
}
