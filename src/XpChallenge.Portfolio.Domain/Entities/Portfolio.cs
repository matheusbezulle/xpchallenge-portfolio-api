using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace XpChallenge.Portfolio.Domain.Entities
{
    public class Portfolio
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; private set; }

        [BsonElement("Nome")]
        public string Nome { get; private set; }

        [BsonElement("Descricao")]
        public string Descricao { get; private set; }

        [BsonElement("Tipo")]
        public string Tipo { get; private set; }
        public IEnumerable<ProdutoFinanceiro> ProdutosFinanceiros { get; private set; } = [];

        public Portfolio(string nome, string descricao, string tipo)
        {
            Nome = nome;
            Descricao = descricao;
            Tipo = tipo;
        }
    }
}
