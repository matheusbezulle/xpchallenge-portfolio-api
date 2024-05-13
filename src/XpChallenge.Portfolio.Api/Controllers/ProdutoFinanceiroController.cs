using MediatR;
using Microsoft.AspNetCore.Mvc;
using XpChallenge.Portfolio.Api.Requests;
using XpChallenge.Portfolio.Application.Commands.AlterarPesoProdutoFinanceiro;
using XpChallenge.Portfolio.Application.Commands.IncluirProdutoFinanceiro;
using XpChallenge.Portfolio.Application.Commands.RemoverProdutoFinanceiro;
using XpChallenge.Portfolio.Application.Notifications;

namespace XpChallenge.Portfolio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoFinanceiroController(IMediator mediator,
        INotificator notificator) : ControllerBase(notificator)
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Método responsável por incluir novos produtos financeiros em um determinado portfólio
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> IncluirProdutoFinanceiroAsync([FromBody] IncluirProdutoFinanceiroRequest request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new IncluirProdutoFinanceiroCommand(request.IdPortfolio, request.Nome, request.IdCategoria, request.Peso, request.DataVencimento), cancellationToken);
            return ProcessarRetorno();
        }

        /// <summary>
        /// Método responsável por alterar o peso de um produto financeiro em determinado portfólio
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("[controller]/Peso")]
        public async Task<IActionResult> AlterarPesoAsync([FromBody] AlterarPesoProdutoFinanceiroRequest request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new AlterarPesoProdutoFinanceiroCommand(request.IdPortfolio, request.Nome, request.Peso), cancellationToken);
            return ProcessarRetorno();
        }

        /// <summary>
        /// Método responsável por remover um produto financeiro de determinado portfólio
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> RemoverProdutoAsync([FromBody] RemoverProdutoFinanceiroRequest request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new RemoverProdutoFinanceiroCommand(request.IdPortfolio, request.Nome), cancellationToken);
            return ProcessarRetorno();
        }
    }
}
