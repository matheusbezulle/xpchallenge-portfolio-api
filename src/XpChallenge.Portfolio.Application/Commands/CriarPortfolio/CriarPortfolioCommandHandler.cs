using MediatR;
using XpChallenge.Portfolio.Application.Services.Interfaces;
using XpChallenge.Portfolio.Domain.ValueObjects;
using Dominio = XpChallenge.Portfolio.Domain.Entities;

namespace XpChallenge.Portfolio.Application.Commands.CriarPortfolio
{
    public class CriarPortfolioCommandHandler(IPortfolioService portfolioService) : IRequestHandler<CriarPortfolioCommand, CriarPortfolioCommandResponse>
    {
        private readonly IPortfolioService _portfolioService = portfolioService;

        public async Task<CriarPortfolioCommandResponse> Handle(CriarPortfolioCommand request, CancellationToken cancellationToken)
        {
            var response = new CriarPortfolioCommandResponse();

            var portfolio = new Dominio.Portfolio(request.Nome, (PortfolioPerfil)request.IdPerfil);

            var id = await _portfolioService.CriarAsync(portfolio, cancellationToken);

            if (id == null)
            {
                //add msg erro 
                return response;
            }

            return new(id);
        }
    }
}
