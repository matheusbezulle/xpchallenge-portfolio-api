using XpChallenge.Portfolio.Domain.AggregateRoots;

namespace XpChallenge.Portfolio.Infra.Mongo.Repositories.Interfaces
{
    public interface IAdministradorRepository
    {
        Task<Administrador> ObterPorEmailAsync(string email, CancellationToken cancellationToken);
        Task<IEnumerable<Administrador>> ObterTodosAsync(CancellationToken cancellationToken);
        Task CriarAsync(Administrador administrador, CancellationToken cancellationToken);
        Task ExcluirAsync(Administrador administrador, CancellationToken cancellationToken);
    }
}
