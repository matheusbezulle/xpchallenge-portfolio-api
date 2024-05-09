using MediatR;

namespace XpChallenge.Portfolio.Application.Commands.CriarPortfolio
{
    public class CriarPortfolioCommand(string nome, string descricao) : IRequest<CriarPortfolioCommandResponse>
    {
        public string Nome { get; set; } = nome;
        public string Descricao { get; set; } = descricao;
    }
}
