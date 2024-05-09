using System.ComponentModel.DataAnnotations;

namespace XpChallenge.Portfolio.Api.Requests
{
    public class AlterarPesoProdutoFinanceiroRequest
    {
        [Required(ErrorMessage = "O campo 'IdPortfolio' é obrigatório.")]
        public string IdPortfolio { get; set; }

        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo 'Peso' é obrigatório.")]
        [Range(0, int.MaxValue, ErrorMessage = "O campo 'Peso' deve ser maior que zero.")]
        public int Peso { get; set; }
    }
}
