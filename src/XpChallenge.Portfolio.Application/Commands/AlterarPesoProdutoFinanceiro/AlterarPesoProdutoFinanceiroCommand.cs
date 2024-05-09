using MediatR;

namespace XpChallenge.Portfolio.Application.Commands.AlterarPesoProdutoFinanceiro
{
    public class AlterarPesoProdutoFinanceiroCommand(string idPortfolio, string nome, int peso) : IRequest<AlterarPesoProdutoFinanceiroCommandResponse>
    {
        public string IdPortfolio { get; set; } = idPortfolio;
        public string Nome { get; set; } = nome;
        public int Peso { get; set; } = peso;
    }
}
