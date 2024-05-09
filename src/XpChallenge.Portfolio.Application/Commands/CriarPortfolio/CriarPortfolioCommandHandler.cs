using MediatR;
using XpChallenge.Portfolio.Application.Services.Interfaces;
using Dominio = XpChallenge.Portfolio.Domain.Entities;

namespace XpChallenge.Portfolio.Application.Commands.CriarPortfolio
{
    public class CriarPortfolioCommandHandler(IPortfolioService portfolioService) : IRequestHandler<CriarPortfolioCommand, CriarPortfolioCommandResponse>
    {
        private readonly IPortfolioService _portfolioService = portfolioService;

        public async Task<CriarPortfolioCommandResponse> Handle(CriarPortfolioCommand request, CancellationToken cancellationToken)
        {
            var portfolio = new Dominio.Portfolio(request.Nome, request.Descricao, string.Empty);

            var id = await _portfolioService.CriarAsync(portfolio, cancellationToken);

            if (id == Guid.Empty)
            {
                //erro
            }

            return new(id);
        }
    }
}
