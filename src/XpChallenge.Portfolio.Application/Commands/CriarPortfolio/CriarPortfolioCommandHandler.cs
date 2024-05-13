using MediatR;
using XpChallenge.Portfolio.Application.Notifications;
using XpChallenge.Portfolio.Application.Services.Interfaces;
using XpChallenge.Portfolio.Domain.ValueObjects;
using Dominio = XpChallenge.Portfolio.Domain.AggregateRoots;

namespace XpChallenge.Portfolio.Application.Commands.CriarPortfolio
{
    public class CriarPortfolioCommandHandler(IPortfolioService portfolioService,
        INotificator notificator) : IRequestHandler<CriarPortfolioCommand, CriarPortfolioCommandResponse>
    {
        private readonly IPortfolioService _portfolioService = portfolioService;
        private readonly INotificator _notificator = notificator;

        public async Task<CriarPortfolioCommandResponse> Handle(CriarPortfolioCommand request, CancellationToken cancellationToken)
        {
            var response = new CriarPortfolioCommandResponse();

            var portfolio = new Dominio.Portfolio(request.Nome, (PortfolioPerfil)request.IdPerfil);

            var id = await _portfolioService.CriarAsync(portfolio, cancellationToken);

            if (id == null)
            {
                _notificator.AdicionarErroAplicacao("Não foi possível cadastrar o portfolio. Tente novamente.");
                response.Mensagens = _notificator.ObterMensagens();
                return response;
            }

            return new(id);
        }
    }
}
