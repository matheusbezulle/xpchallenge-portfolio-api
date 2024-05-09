using MediatR;

namespace XpChallenge.Portfolio.Application.Commands.ExcluirPortfolio
{
    public class ExcluirPortfolioCommand(string idPortfolio) : IRequest<ExcluirPortfolioCommandResponse>
    {
        public string IdPortfolio { get; set; } = idPortfolio;
    }
}
