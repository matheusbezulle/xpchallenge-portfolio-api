using XpChallenge.Portfolio.Domain.AggregateRoots;
using Dominio = XpChallenge.Portfolio.Domain.AggregateRoots;

namespace XpChallenge.Portfolio.Application.Services.Interfaces
{
    public interface IEmailService
    {
        void NotificarAdministradores(IEnumerable<Dominio.Portfolio> portfolios, IEnumerable<Administrador> administradores);
    }
}
