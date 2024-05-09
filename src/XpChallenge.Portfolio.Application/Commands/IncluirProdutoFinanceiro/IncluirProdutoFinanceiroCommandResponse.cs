namespace XpChallenge.Portfolio.Application.Commands.IncluirProdutoFinanceiro
{
    public class IncluirProdutoFinanceiroCommandResponse(bool sucesso = false)
    {
        public bool Sucesso { get; set; } = sucesso;
    }
}