using MediatR;

namespace XpChallenge.Portfolio.Application.Commands.CriarPortfolio
{
    public class CriarPortfolioCommand(string nome, int idPerfil) : IRequest<CriarPortfolioCommandResponse>
    {
        public string Nome { get; set; } = nome;
        public int IdPerfil { get; set; } = idPerfil;
    }
}
