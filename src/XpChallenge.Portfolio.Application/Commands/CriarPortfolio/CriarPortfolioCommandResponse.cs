using XpChallenge.Portfolio.Application.DataTransfer;

namespace XpChallenge.Portfolio.Application.Commands.CriarPortfolio
{
    public class CriarPortfolioCommandResponse(string? id = null) : ResponseBaseDto
    {
        public string Id { get; set; } = id ?? string.Empty;
    }
}