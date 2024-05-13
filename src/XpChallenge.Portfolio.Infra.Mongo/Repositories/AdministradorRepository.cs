using MongoDB.Driver;
using XpChallenge.Portfolio.Domain.AggregateRoots;
using XpChallenge.Portfolio.Infra.Mongo.Repositories.Interfaces;

namespace XpChallenge.Portfolio.Infra.Mongo.Repositories
{
    public class AdministradorRepository(IMongoDatabase database) : IAdministradorRepository
    {
        private readonly IMongoCollection<Administrador> _administradorCollection = database.GetCollection<Administrador>("Administradores");

        public async Task<Administrador> ObterPorEmailAsync(string email, CancellationToken cancellationToken)
        {
            var administrador = await _administradorCollection.Find(a => a.Email.Equals(email)).FirstOrDefaultAsync(cancellationToken);
            return administrador;
        }

        public async Task<IEnumerable<Administrador>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var filter = Builders<Administrador>.Filter.Empty;
            
            return await _administradorCollection.Find(filter)
                .ToListAsync();
        }

        public async Task CriarAsync(Administrador administrador, CancellationToken cancellationToken)
        {
            await _administradorCollection.InsertOneAsync(administrador, cancellationToken: cancellationToken);
        }

        public async Task ExcluirAsync(Administrador administrador, CancellationToken cancellationToken)
        {
            var result = await _administradorCollection.DeleteOneAsync(p => p.Id == administrador.Id, cancellationToken: cancellationToken);

            if (!result.IsAcknowledged || result.DeletedCount == 0)
                throw new Exception("Não foi possível excluir o administrador na base. Tente novamente.");
        }
    }
}
