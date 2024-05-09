using MediatR;
using MongoDB.Bson;
using XpChallenge.Portfolio.Application.Services.Interfaces;

namespace XpChallenge.Portfolio.Application.Commands.ExcluirPortfolio
{
    public class ExcluirPortfolioCommandHandler(IPortfolioService portfolioService) : IRequestHandler<ExcluirPortfolioCommand, ExcluirPortfolioCommandResponse>
    {
        private readonly IPortfolioService _portfolioService = portfolioService;

        public async Task<ExcluirPortfolioCommandResponse> Handle(ExcluirPortfolioCommand request, CancellationToken cancellationToken)
        {
            if (!ObjectId.TryParse(request.IdPortfolio, out var id))
            {
                //erro
            }

            await _portfolioService.ExcluirAsync(id, cancellationToken);

            return new(true);
        }
    }
}
