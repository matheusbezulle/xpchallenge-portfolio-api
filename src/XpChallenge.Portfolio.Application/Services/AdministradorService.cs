using System.Net.Mail;
using System.Net;
using XpChallenge.Portfolio.Application.Services.Interfaces;
using XpChallenge.Portfolio.Domain.AggregateRoots;
using XpChallenge.Portfolio.Infra.Mongo.Repositories.Interfaces;
using XpChallenge.Portfolio.Mongo.Repositories.Interfaces;
using System.Text;

namespace XpChallenge.Portfolio.Application.Services
{
    public class AdministradorService(IEmailService emailService,
        IAdministradorRepository administradorRepository,
        IPortfolioRepository portfolioRepository) : IAdministradorService
    {
        private readonly IEmailService emailService = emailService;
        private readonly IAdministradorRepository _administradorRepository = administradorRepository;
        private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;

        public async Task<Administrador> ObterPorEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _administradorRepository.ObterPorEmailAsync(email, cancellationToken);
        }

        public async Task CriarAsync(Administrador administrador, CancellationToken cancellationToken)
        {
            await _administradorRepository.CriarAsync(administrador, cancellationToken);
        }

        public async Task ExcluirAsync(Administrador administrador, CancellationToken cancellationToken)
        {
            await _administradorRepository.ExcluirAsync(administrador, cancellationToken);
        }

        public async Task NotificarProdutosProximosAoVencimento(CancellationToken cancellationToken = default)
        {
            var administradores = await _administradorRepository.ObterTodosAsync(cancellationToken);

            if (!administradores.Any())
                return;

            var produtosProximosAoVencimento = await _portfolioRepository.ObterProximosAoVencimentoAsync(cancellationToken);

            if (!produtosProximosAoVencimento.Any())
                return;

            emailService.NotificarAdministradores(produtosProximosAoVencimento, administradores);
        }
    }
}
