using MongoDB.Bson;
using XpChallenge.Portfolio.Application.Services.Interfaces;
using XpChallenge.Portfolio.MongoDb.Repositories.Interfaces;
using Dominio = XpChallenge.Portfolio.Domain.AggregateRoots;

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

        public async Task AtualizarAsync(Dominio.Portfolio portfolio, CancellationToken cancellationToken)
        {
            await _portfolioRepository.AtualizarAsync(portfolio, cancellationToken);
        }

        public async Task ExcluirAsync(ObjectId id, CancellationToken cancellationToken)
        {
            await _portfolioRepository.ExcluirAsync(id, cancellationToken);
        }

        public async Task<Dominio.Portfolio> ObterPorIdAsync(ObjectId id, CancellationToken cancellationToken)
        {
            return await _portfolioRepository.ObterPorIdAsync(id, cancellationToken);
        }
    }
}
