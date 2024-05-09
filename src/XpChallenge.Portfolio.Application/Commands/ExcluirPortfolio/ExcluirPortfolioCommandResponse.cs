namespace XpChallenge.Portfolio.Application.Commands.ExcluirPortfolio
{
    public class ExcluirPortfolioCommandResponse(bool sucesso = false)
    {
        public bool Sucesso { get; set; } = sucesso;
    }
}