namespace XpChallenge.Portfolio.Application.DataTransfer
{
    public class ProdutoFinanceiroDto
    {
        public string Nome { get; set; } = string.Empty;
        public int IdCategoria { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public int Peso { get; set; }
        public double Porcentagem { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}