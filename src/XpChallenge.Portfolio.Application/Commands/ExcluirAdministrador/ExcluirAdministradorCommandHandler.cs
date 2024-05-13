using MediatR;
using XpChallenge.Portfolio.Application.Notifications;
using XpChallenge.Portfolio.Application.Services.Interfaces;

namespace XpChallenge.Portfolio.Application.Commands.ExcluirAdministrador
{
    public class ExcluirAdministradorCommandHandler(IAdministradorService administradorService,
        INotificator notificator) : IRequestHandler<ExcluirAdministradorCommand, ExcluirAdministradorCommandResponse>
    {
        private readonly IAdministradorService _administradorService = administradorService;
        private readonly INotificator _notificator = notificator;

        public async Task<ExcluirAdministradorCommandResponse> Handle(ExcluirAdministradorCommand request, CancellationToken cancellationToken)
        {
            var response = new ExcluirAdministradorCommandResponse();

            var administradorExistente = await _administradorService.ObterPorEmailAsync(request.Email, cancellationToken);

            if (administradorExistente == null)
            {
                _notificator.AdicionarErroNegocio("Não foi encontrado nenhum administrador com o email informado.");
                return response;
            }

            await _administradorService.ExcluirAsync(administradorExistente, cancellationToken);

            return response;
        }
    }
}
