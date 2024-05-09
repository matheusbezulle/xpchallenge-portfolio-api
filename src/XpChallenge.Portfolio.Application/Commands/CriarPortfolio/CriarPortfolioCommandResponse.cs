namespace XpChallenge.Portfolio.Application.Commands.CriarPortfolio
{
    public class CriarPortfolioCommandResponse(Guid id)
    {
        public Guid Id { get; set; } = id;
    }
}