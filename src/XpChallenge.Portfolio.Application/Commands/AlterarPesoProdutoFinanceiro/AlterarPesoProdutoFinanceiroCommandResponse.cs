namespace XpChallenge.Portfolio.Application.Commands.AlterarPesoProdutoFinanceiro
{
    public class AlterarPesoProdutoFinanceiroCommandResponse(bool sucesso = false)
    {
        public bool Sucesso { get; set; } = sucesso;
    }
}