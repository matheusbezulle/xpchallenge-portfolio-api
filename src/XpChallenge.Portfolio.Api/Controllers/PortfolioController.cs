using MediatR;
using Microsoft.AspNetCore.Mvc;
using XpChallenge.Portfolio.Api.Requests;
using XpChallenge.Portfolio.Application.Commands.CriarPortfolio;
using XpChallenge.Portfolio.Application.Commands.ExcluirPortfolio;
using XpChallenge.Portfolio.Application.Notifications;
using XpChallenge.Portfolio.Application.Queries.ObterPortfolio;

namespace XpChallenge.Portfolio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PortfolioController(IMediator mediator,
        INotificator notificator) : ControllerBase(notificator)
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Obter os dados de determinado portfólio
        /// </summary>
        /// <param name="idPortfolio"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{idPortfolio}")]
        public async Task<IActionResult> ObterPortfolioAsync(string idPortfolio, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new ObterPortfolioQuery(idPortfolio), cancellationToken);
            return ProcessarRetorno(result);
        }

        /// <summary>
        /// Método responsável por cadastrar um novo portfólio
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CriarPortfolioAsync([FromBody] CriarPortfolioRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new CriarPortfolioCommand(request.Nome, request.IdPerfil), cancellationToken);
            return ProcessarRetorno(result);
        }

        /// <summary>
        /// Método responsável por excluir um portfólio
        /// </summary>
        /// <param name="idPortfolio"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{idPortfolio}")]
        public async Task<IActionResult> ExcluirPortfolioAsync(string idPortfolio, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new ExcluirPortfolioCommand(idPortfolio), cancellationToken);
            return ProcessarRetorno();
        }
    }
}
