using MongoDB.Bson;
using Dominio = XpChallenge.Portfolio.Domain.Entities;

namespace XpChallenge.Portfolio.Application.Services.Interfaces
{
    public interface IPortfolioService
    {
        Task<string> CriarAsync(Dominio.Portfolio portfolio, CancellationToken cancellationToken);
        Task ExcluirAsync(ObjectId id, CancellationToken cancellationToken);
    }
}
