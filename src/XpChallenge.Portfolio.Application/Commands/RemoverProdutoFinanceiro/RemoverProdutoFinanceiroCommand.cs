using MediatR;

namespace XpChallenge.Portfolio.Application.Commands.RemoverProdutoFinanceiro
{
    public class RemoverProdutoFinanceiroCommand(string idPortfolio, string nome) : IRequest<RemoverProdutoFinanceiroCommandResponse>
    {
        public string IdPortfolio { get; set; } = idPortfolio;
        public string Nome { get; set; } = nome;
    }
}
