using MediatR;
using MongoDB.Bson;
using XpChallenge.Portfolio.Application.Notifications;
using XpChallenge.Portfolio.Application.Services.Interfaces;

namespace XpChallenge.Portfolio.Application.Commands.ExcluirPortfolio
{
    public class ExcluirPortfolioCommandHandler(IPortfolioService portfolioService,
        INotificator notificator) : IRequestHandler<ExcluirPortfolioCommand, ExcluirPortfolioCommandResponse>
    {
        private readonly IPortfolioService _portfolioService = portfolioService;
        private readonly INotificator _notificator = notificator;

        public async Task<ExcluirPortfolioCommandResponse> Handle(ExcluirPortfolioCommand request, CancellationToken cancellationToken)
        {
            var response = new ExcluirPortfolioCommandResponse();

            if (!ObjectId.TryParse(request.IdPortfolio, out var id))
            {
                _notificator.AdicionarErroNegocio("O 'IdPortfolio' informado é inválido.");
                return response;
            }

            await _portfolioService.ExcluirAsync(id, cancellationToken);

            response.Sucesso = true;
            return response;
        }
    }
}
