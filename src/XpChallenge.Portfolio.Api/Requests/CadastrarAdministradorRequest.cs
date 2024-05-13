using System.ComponentModel.DataAnnotations;

namespace XpChallenge.Portfolio.Api.Requests
{
    public class CadastrarAdministradorRequest
    {
        [Required(ErrorMessage = "O campo 'Email' é obrigatório.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Endereço de email inválido.")]
        public string Email { get; set; }
    }
}
