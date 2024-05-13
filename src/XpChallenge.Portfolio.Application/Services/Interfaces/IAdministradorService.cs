using XpChallenge.Portfolio.Domain.AggregateRoots;

namespace XpChallenge.Portfolio.Application.Services.Interfaces
{
    public interface IAdministradorService
    {
        Task<Administrador> ObterPorEmailAsync(string email, CancellationToken cancellationToken);
        Task CriarAsync(Administrador administrador, CancellationToken cancellationToken);
        Task ExcluirAsync(Administrador administrador, CancellationToken cancellationToken);
        Task NotificarProdutosProximosAoVencimento(CancellationToken cancellationToken = default);
    }
}
