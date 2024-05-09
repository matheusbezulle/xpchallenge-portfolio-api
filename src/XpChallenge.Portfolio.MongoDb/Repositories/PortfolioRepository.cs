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

        public async Task ExcluirAsync(ObjectId id, CancellationToken cancellationToken)
        {
            var result = await _portfolioCollection.DeleteOneAsync(p => p.Id == id, cancellationToken: cancellationToken);

            if (!result.IsAcknowledged || result.DeletedCount == 0)
                throw new Exception("Não foi possível excluir o portfolio. Tente novamente.");
        }
    }
}
