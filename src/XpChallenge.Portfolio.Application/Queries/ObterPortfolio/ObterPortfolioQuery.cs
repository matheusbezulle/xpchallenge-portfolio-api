using MediatR;

namespace XpChallenge.Portfolio.Application.Queries.ObterPortfolio
{
    public class ObterPortfolioQuery(string idPortfolio) : IRequest<ObterPortfolioQueryResponse>
    {
        public string IdPortfolio { get; set; } = idPortfolio;
    }
}
