using XpChallenge.Portfolio.Application.DataTransfer;

namespace XpChallenge.Portfolio.Application.Queries.ObterPortfolio
{
    public class ObterPortfolioQueryResponse(PortfolioDto? portfolioDto = null) : ResponseBaseDto
    {
        public PortfolioDto? Portfolio { get; set; } = portfolioDto;
    }
}