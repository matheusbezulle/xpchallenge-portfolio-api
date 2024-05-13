using MongoDB.Bson;
using Dominio = XpChallenge.Portfolio.Domain.AggregateRoots;

namespace XpChallenge.Portfolio.Mongo.Repositories.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<Dominio.Portfolio> ObterPorIdAsync(ObjectId id, CancellationToken cancellationToken);
        Task<IEnumerable<Dominio.Portfolio>> ObterProximosAoVencimentoAsync(CancellationToken cancellationToken);
        Task<ObjectId> CriarAsync(Dominio.Portfolio portfolio, CancellationToken cancellationToken);
        Task AtualizarAsync(Dominio.Portfolio portfolio, CancellationToken cancellationToken);
        Task ExcluirAsync(ObjectId id, CancellationToken cancellationToken);
    }
}
