namespace XpChallenge.Portfolio.Application.DataTransfer
{
    public class ProdutoFinanceiroDto
    {
        public string Nome { get; private set; } = string.Empty;
        public int IdCategoria { get; set; }
        public string Categoria { get; private set; } = string.Empty;
        public int Peso { get; private set; }
        public DateTime DataVencimento { get; set; }
    }
}