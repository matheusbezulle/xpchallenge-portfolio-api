using System.Net.Mail;
using System.Net;
using XpChallenge.Portfolio.Application.Services.Interfaces;
using XpChallenge.Portfolio.Domain.AggregateRoots;
using XpChallenge.Portfolio.Infra.Mongo.Repositories.Interfaces;
using XpChallenge.Portfolio.Mongo.Repositories.Interfaces;
using System.Text;

namespace XpChallenge.Portfolio.Application.Services
{
    public class AdministradorService(IAdministradorRepository administradorRepository,
        IPortfolioRepository portfolioRepository) : IAdministradorService
    {
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

            var fromAddress = new MailAddress("matheusbezulle.brawl@gmail.com", "Matheus Brawl");
            const string fromPassword = "tempteste02";
            const string subject = "Produtos financeiros próximos ao vencimento";
            var body = new StringBuilder("");

            foreach (var produto in produtosProximosAoVencimento)
            {
                body.AppendLine($"\nPortfólio {produto.Nome}:");
                foreach (var p in produto.ProdutosFinanceiros)
                {
                    body.AppendLine($" {p.Nome} vence em {p.DataVencimento.ToString()};");
                }
            }

            Parallel.ForEach(administradores, adm =>
            {
                var toAddress = new MailAddress(adm.Email, "Matheus Bezulle");

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 60 * 60
                };
                using var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body.ToString()
                };
                smtp.Send(message);
            });
        }
    }
}
