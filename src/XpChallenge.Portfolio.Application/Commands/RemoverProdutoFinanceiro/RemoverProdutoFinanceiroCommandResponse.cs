namespace XpChallenge.Portfolio.Application.Commands.RemoverProdutoFinanceiro
{
    public class RemoverProdutoFinanceiroCommandResponse(bool sucesso = false)
    {
        public bool Sucesso { get; set; } = sucesso;
    }
}