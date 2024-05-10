namespace XpChallenge.Portfolio.Domain.ValueObjects
{
    public class ProdutoFinanceiro
    {
        public string Nome { get; private set; } = string.Empty;
        public ProdutoCategoria Categoria { get; private set; }
        public int Peso { get; private set; }
        public DateTime DataVencimento { get; private set; }

        public void AtualizarPeso(int peso)
        {
            Peso = peso;
        }

        public void AtualizarDataVencimento(DateTime dataVencimento)
        {
            DataVencimento = dataVencimento;
        }
    }
}