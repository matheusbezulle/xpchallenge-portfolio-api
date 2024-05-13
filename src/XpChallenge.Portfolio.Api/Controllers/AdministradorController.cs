using MediatR;
using Microsoft.AspNetCore.Mvc;
using XpChallenge.Portfolio.Api.Requests;
using XpChallenge.Portfolio.Application.Commands.CadastrarAdministrador;
using XpChallenge.Portfolio.Application.Commands.ExcluirAdministrador;
using XpChallenge.Portfolio.Application.Notifications;

namespace XpChallenge.Portfolio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdministradorController(IMediator mediator,
        INotificator notificator) : ControllerBase(notificator)
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Método responsável por cadastrar um administrador
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CadastrarAdministradorAsync([FromBody] CadastrarAdministradorRequest request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new CadastrarAdministradorCommand(request.Email), cancellationToken);
            return ProcessarRetorno();
        }

        /// <summary>
        /// Método responsável por excluir um administrador
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{email}")]
        public async Task<IActionResult> ExcluirAdministradorAsync(string email, CancellationToken cancellationToken)
        {
            await _mediator.Send(new ExcluirAdministradorCommand(email), cancellationToken);
            return ProcessarRetorno();
        }
    }
}
