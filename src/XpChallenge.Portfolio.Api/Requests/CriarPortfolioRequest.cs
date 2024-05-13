using System.ComponentModel.DataAnnotations;

namespace XpChallenge.Portfolio.Api.Requests
{
    public class CriarPortfolioRequest
    {
        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo 'IdPerfil' é obrigatório.")]
        [Range(1, 3, ErrorMessage = "O campo 'IdPerfil' deve ser preenchido com valores de 1-Conservador, 2-Moderado e 3-Agressivo.")]
        public int IdPerfil { get; set; }
    }
}
