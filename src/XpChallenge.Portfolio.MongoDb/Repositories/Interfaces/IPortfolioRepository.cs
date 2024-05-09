using MongoDB.Bson;
using Dominio = XpChallenge.Portfolio.Domain.Entities;

namespace XpChallenge.Portfolio.MongoDb.Repositories.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<ObjectId> CriarAsync(Dominio.Portfolio portfolio, CancellationToken cancellationToken);
        Task ExcluirAsync(ObjectId id, CancellationToken cancellationToken);
        Task<Dominio.Portfolio> ObterPorIdAsync(ObjectId id, CancellationToken cancellationToken);
    }
}
