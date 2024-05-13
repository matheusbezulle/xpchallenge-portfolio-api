using System.Net.Mail;
using System.Net;
using System.Text;
using XpChallenge.Portfolio.Application.Services.Interfaces;
using Dominio = XpChallenge.Portfolio.Domain.AggregateRoots;
using XpChallenge.Portfolio.Domain.AggregateRoots;

namespace XpChallenge.Portfolio.Application.Services
{
    public class EmailService : IEmailService
    {
        public void NotificarAdministradores(IEnumerable<Dominio.Portfolio> portfolios, IEnumerable<Administrador> administradores)
        {
            var fromAddress = new MailAddress("matheusbezulle.brawl@gmail.com", "Matheus Brawl");
            const string fromPassword = "tempteste05";
            const string subject = "Produtos financeiros próximos ao vencimento";
            var body = new StringBuilder("");

            foreach (var portfolio in portfolios)
            {
                body.AppendLine($"\nPortfólio {portfolio.Nome}:");
                foreach (var produtoFinanceiro in portfolio.ProdutosFinanceiros)
                {
                    body.AppendLine($" {produtoFinanceiro.Nome} vence em {produtoFinanceiro.DataVencimento.ToString()};");
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
