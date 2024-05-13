using MediatR;
using XpChallenge.Portfolio.Application.Notifications;
using XpChallenge.Portfolio.Application.Services.Interfaces;
using XpChallenge.Portfolio.Domain.AggregateRoots;

namespace XpChallenge.Portfolio.Application.Commands.CadastrarAdministrador
{
    public class CadastrarAdministradorCommandHandler(IAdministradorService administradorService,
        INotificator notificator) : IRequestHandler<CadastrarAdministradorCommand, CadastrarAdministradorCommandResponse>
    {
        private readonly IAdministradorService _administradorService = administradorService;
        private readonly INotificator _notificator = notificator;

        public async Task<CadastrarAdministradorCommandResponse> Handle(CadastrarAdministradorCommand request, CancellationToken cancellationToken)
        {
            var response = new CadastrarAdministradorCommandResponse();

            var administradorExistente = await _administradorService.ObterPorEmailAsync(request.Email, cancellationToken);

            if (administradorExistente != null)
            {
                _notificator.AdicionarErroNegocio("O email informado já está cadastrado como administrador.");
                return response;
            }

            await _administradorService.CriarAsync(new Administrador(request.Email), cancellationToken);

            return response;
        }
    }
}
