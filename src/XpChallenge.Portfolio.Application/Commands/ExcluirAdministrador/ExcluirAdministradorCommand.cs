using MediatR;

namespace XpChallenge.Portfolio.Application.Commands.ExcluirAdministrador
{
    public class ExcluirAdministradorCommand(string email) : IRequest<ExcluirAdministradorCommandResponse>
    {
        public string Email { get; set; } = email;
    }
}
