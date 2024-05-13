using MongoDB.Bson;
using MongoDB.Driver;
using XpChallenge.Portfolio.Mongo.Repositories.Interfaces;
using Dominio = XpChallenge.Portfolio.Domain.AggregateRoots;

namespace XpChallenge.Portfolio.Mongo.Repositories
{
    public class PortfolioRepository(IMongoDatabase database) : IPortfolioRepository
    {
        private readonly IMongoCollection<Dominio.Portfolio> _portfolioCollection = database.GetCollection<Dominio.Portfolio>("Portfolios");

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

        public async Task<IEnumerable<Dominio.Portfolio>> ObterProximosAoVencimentoAsync(CancellationToken cancellationToken)
        {
            var dataReferencia = DateTime.Now.AddDays(2);

            var filter = Builders<Dominio.Portfolio>.Filter.ElemMatch(p => p.ProdutosFinanceiros, pf => pf.DataVencimento <= dataReferencia);
            var portfolios = await _portfolioCollection.Find(filter).ToListAsync(cancellationToken);

            foreach (var portfolio in portfolios)
            {
                portfolio.ProdutosFinanceiros.RemoveAll(p => p.DataVencimento > dataReferencia);
            }

            return portfolios;
        }
    }
}
