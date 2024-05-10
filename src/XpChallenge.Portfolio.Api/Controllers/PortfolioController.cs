using MediatR;
using Microsoft.AspNetCore.Mvc;
using XpChallenge.Portfolio.Api.Requests;
using XpChallenge.Portfolio.Application.Commands.CriarPortfolio;
using XpChallenge.Portfolio.Application.Commands.ExcluirPortfolio;
using XpChallenge.Portfolio.Application.Notifications;
using XpChallenge.Portfolio.Application.Queries.ObterPortfolio;

namespace XpChallenge.Portfolio.Api.Controllers
{
    /// <summary>
    /// Controller responsável por métodos de gerenciamento dos portfólios
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="notificator"></param>
    [ApiController]
    [Route("[controller]")]
    public class PortfolioController(IMediator mediator,
        INotificator notificator) : ControllerBase(notificator)
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Método responsável por cadastrar um novo portfólio
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CriarPortfolio([FromBody] CriarPortfolioRequest request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new CriarPortfolioCommand(request.Nome, request.IdPerfil), cancellationToken);
            return ProcessarRetorno();
        }

        /// <summary>
        /// Método responsável por excluir um portfólio
        /// </summary>
        /// <param name="idPortfolio"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{idPortfolio}")]
        public async Task<IActionResult> ExcluirPortfolio(string idPortfolio, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new ExcluirPortfolioCommand(idPortfolio), cancellationToken);
            return ProcessarRetorno();
        }

        /// <summary>
        /// Método para obter os dados de determinado portfólio
        /// </summary>
        /// <param name="idPortfolio"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{idPortfolio}")]
        public async Task<IActionResult> ObterPortfolio(string idPortfolio, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new ObterPortfolioQuery(idPortfolio), cancellationToken);
            return ProcessarRetorno(result);
        }
    }
}
