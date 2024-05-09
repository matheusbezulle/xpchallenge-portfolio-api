using MongoDB.Bson;
using MongoDB.Driver;
using XpChallenge.Portfolio.MongoDb.Repositories.Interfaces;
using Dominio = XpChallenge.Portfolio.Domain.Entities;

namespace XpChallenge.Portfolio.MongoDb.Repositories
{
    public class PortfolioRepository(IMongoDatabase database) : IPortfolioRepository
    {
        private readonly IMongoCollection<Dominio.Portfolio> _portfolioCollection = database.GetCollection<Dominio.Portfolio>("portfolios");

        public async Task<ObjectId> CriarAsync(Dominio.Portfolio portfolio, CancellationToken cancellationToken)
        {
            await _portfolioCollection.InsertOneAsync(portfolio, cancellationToken: cancellationToken);
            return portfolio.Id;
        }

        public async Task AtualizarAsync(Dominio.Portfolio portfolio, CancellationToken cancellationToken)
        {
            var filter = Builders<Dominio.Portfolio>.Filter
                .Eq(p => p.Id, portfolio.Id);
            
            var update = Builders<Dominio.Portfolio>.Update
                .Set(p => p.Nome, portfolio.Nome)
                .Set(p => p.Perfil, portfolio.Perfil)
                .Set(p => p.ProdutosFinanceiros, portfolio.ProdutosFinanceiros);

            var result = await _portfolioCollection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

            if (!result.IsAcknowledged)
                throw new Exception("Não foi possível atualizar o portfolio na base. Tente novamente.");
        }

        public async Task ExcluirAsync(ObjectId id, CancellationToken cancellationToken)
        {
            var result = await _portfolioCollection.DeleteOneAsync(p => p.Id == id, cancellationToken: cancellationToken);

            if (!result.IsAcknowledged || result.DeletedCount == 0)
                throw new Exception("Não foi possível excluir o portfolio na base. Tente novamente.");
        }

        public async Task<Dominio.Portfolio> ObterPorIdAsync(ObjectId id, CancellationToken cancellationToken)
        {
            var portfolio = await _portfolioCollection.Find(p => p.Id.Equals(id)).FirstOrDefaultAsync(cancellationToken);
            return portfolio;
        }
    }
}
