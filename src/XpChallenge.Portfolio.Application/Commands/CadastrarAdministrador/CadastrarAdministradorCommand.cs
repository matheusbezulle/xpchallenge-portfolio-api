using MediatR;

namespace XpChallenge.Portfolio.Application.Commands.CadastrarAdministrador
{
    public class CadastrarAdministradorCommand(string email) : IRequest<CadastrarAdministradorCommandResponse>
    {
        public string Email { get; set; } = email;
    }
}
