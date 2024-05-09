using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using XpChallenge.Portfolio.Domain.ValueObjects;

namespace XpChallenge.Portfolio.Domain.Entities
{
    public class Portfolio(string nome, int perfil)
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; private set; }

        [BsonElement("Nome")]
        public string Nome { get; private set; } = nome;

        [BsonElement("Perfil")]
        public PortfolioPerfil Perfil { get; private set; } = (PortfolioPerfil)perfil;

        [BsonElement("ProdutosFinanceiros")]
        public IEnumerable<ProdutoFinanceiro> ProdutosFinanceiros { get; private set; } = [];
    }
}
