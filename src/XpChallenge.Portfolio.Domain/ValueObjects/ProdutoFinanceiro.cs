namespace XpChallenge.Portfolio.Domain.ValueObjects
{
    public class ProdutoFinanceiro(string nome, ProdutoCategoria categoria, int peso, DateTime dataVencimento)
    {
        public string Nome { get; private set; } = nome;
        public ProdutoCategoria Categoria { get; private set; } = categoria;
        public int Peso { get; private set; } = peso;
        public DateTime DataVencimento { get; private set; } = dataVencimento;

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