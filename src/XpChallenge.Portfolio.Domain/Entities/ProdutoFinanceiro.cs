using XpChallenge.Portfolio.Domain.ValueObjects;

namespace XpChallenge.Portfolio.Domain.Entities
{
    public class ProdutoFinanceiro
    {
        public string Nome { get; private set; } = string.Empty;
        public ProdutoCategoria Categoria { get; private set; }
        public int Peso { get; private set; }
    }
}