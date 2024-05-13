using MediatR;

namespace XpChallenge.Portfolio.Application.Commands.AlterarProdutoFinanceiro
{
    public class AlterarProdutoFinanceiroCommand(string idPortfolio, string nome, int peso, DateTime dataVencimento) : IRequest<AlterarProdutoFinanceiroCommandResponse>
    {
        public string IdPortfolio { get; set; } = idPortfolio;
        public string Nome { get; set; } = nome;
        public int Peso { get; set; } = peso;
        public DateTime DataVencimento { get; set; } = dataVencimento;
    }
}
