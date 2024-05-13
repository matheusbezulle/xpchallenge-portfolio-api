using MediatR;

namespace XpChallenge.Portfolio.Application.Commands.IncluirProdutoFinanceiro
{
    public class IncluirProdutoFinanceiroCommand(string idPortfolio, string nome, int idCategoria, int peso, DateTime dataVencimento) : IRequest<IncluirProdutoFinanceiroCommandResponse>
    {
        public string IdPortfolio { get; set; } = idPortfolio;
        public string Nome { get; set; } = nome;
        public int IdCategoria { get; set; } = idCategoria;
        public int Peso { get; set; } = peso;
        public DateTime DataVencimento { get; set; } = dataVencimento;
    }
}
