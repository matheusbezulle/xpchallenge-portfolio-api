namespace XpChallenge.Portfolio.Application.DataTransfer
{
    public class PortfolioDto
    {
        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public int IdPerfil { get; set; }
        public string Perfil { get; set; } = string.Empty;
        public IEnumerable<ProdutoFinanceiroDto> ProdutosFinanceiros { get; set; } = [];
    }
}
