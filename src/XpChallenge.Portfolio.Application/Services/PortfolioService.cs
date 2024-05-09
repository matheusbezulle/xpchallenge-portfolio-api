using MongoDB.Bson;
using XpChallenge.Portfolio.Application.Services.Interfaces;
using XpChallenge.Portfolio.MongoDb.Repositories.Interfaces;
using Dominio = XpChallenge.Portfolio.Domain.Entities;

namespace XpChallenge.Portfolio.Application.Services
{
    public class PortfolioService(IPortfolioRepository portfolioRepository) : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;

        public async Task<string> CriarAsync(Dominio.Portfolio portfolio, CancellationToken cancellationToken)
        {
            var id = await _portfolioRepository.CriarAsync(portfolio, cancellationToken);
            return id.ToString();
        }

        public async Task ExcluirAsync(ObjectId id, CancellationToken cancellationToken)
        {
            await _portfolioRepository.ExcluirAsync(id, cancellationToken);
        }
    }
}
