namespace XpChallenge.Portfolio.Application.Commands.CriarPortfolio
{
    public class CriarPortfolioCommandResponse(string? id = null)
    {
        public string Id { get; set; } = id ?? string.Empty;
    }
}