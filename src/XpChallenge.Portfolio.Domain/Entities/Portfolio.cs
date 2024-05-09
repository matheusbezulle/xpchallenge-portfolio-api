using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using XpChallenge.Portfolio.Domain.ValueObjects;

namespace XpChallenge.Portfolio.Domain.Entities
{
    public class Portfolio(string nome, PortfolioPerfil perfil)
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; private set; }

        [BsonElement("Nome")]
        public string Nome { get; private set; } = nome;

        [BsonElement("Perfil")]
        public PortfolioPerfil Perfil { get; private set; } = perfil;

        [BsonElement("ProdutosFinanceiros")]
        public List<ProdutoFinanceiro> ProdutosFinanceiros { get; private set; } = [];

        public ProdutoFinanceiro? ObterProdutoFinanceiro(string nome)
        {
            return ProdutosFinanceiros.FirstOrDefault(p => string.Equals(p.Nome, nome));
        }

        public void AdicionarProdutoFinanceiro(ProdutoFinanceiro produtoFinanceiro)
        {
            ProdutosFinanceiros.Add(produtoFinanceiro);
        }

        public void RemoverProdutoFinanceiro(ProdutoFinanceiro produtoFinanceiro)
        {
            ProdutosFinanceiros.Remove(produtoFinanceiro);
        }

        public bool VerificarProdutoFinanceiroExistente(string nome)
        {
            return ProdutosFinanceiros.Any(p => string.Equals(p.Nome, nome));
        }
    }
}
