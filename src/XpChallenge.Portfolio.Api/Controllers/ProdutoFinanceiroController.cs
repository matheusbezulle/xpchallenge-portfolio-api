using MediatR;
using Microsoft.AspNetCore.Mvc;
using XpChallenge.Portfolio.Api.Requests;
using XpChallenge.Portfolio.Application.Commands.AlterarPesoProdutoFinanceiro;
using XpChallenge.Portfolio.Application.Commands.IncluirProdutoFinanceiro;
using XpChallenge.Portfolio.Application.Commands.RemoverProdutoFinanceiro;
using XpChallenge.Portfolio.Application.Notifications;

namespace XpChallenge.Portfolio.Api.Controllers
{
    /// <summary>
    /// Controller respons�vel por m�todos de gerenciamento dos produtos financeiros
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="notificator"></param>
    [ApiController]
    [Route("[controller]")]
    public class ProdutoFinanceiroController(IMediator mediator,
        INotificator notificator) : ControllerBase(notificator)
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// M�todo respons�vel por incluir novos produtos financeiros em um determinado portf�lio
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> IncluirProdutoFinanceiro([FromBody] IncluirProdutoFinanceiroRequest request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new IncluirProdutoFinanceiroCommand(request.IdPortfolio, request.Nome, request.IdCategoria, request.Peso, request.DataVencimento), cancellationToken);
            return ProcessarRetorno();
        }

        /// <summary>
        /// M�todo respons�vel por alterar o peso de um produto financeiro em determinado portf�lio
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("[controller]/Peso")]
        public async Task<IActionResult> AlterarPeso([FromBody] AlterarPesoProdutoFinanceiroRequest request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new AlterarPesoProdutoFinanceiroCommand(request.IdPortfolio, request.Nome, request.Peso));
            return ProcessarRetorno();
        }

        /// <summary>
        /// M�todo respons�vel por remover um produto financeiro de determinado portf�lio
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> RemoverProduto([FromBody] RemoverProdutoFinanceiroRequest request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new RemoverProdutoFinanceiroCommand(request.IdPortfolio, request.Nome));
            return ProcessarRetorno();
        }
    }
}
