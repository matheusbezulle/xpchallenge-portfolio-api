using System.ComponentModel.DataAnnotations;

namespace XpChallenge.Portfolio.Api.Requests
{
    public class RemoverProdutoFinanceiroRequest
    {
        [Required(ErrorMessage = "O campo 'IdPortfolio' é obrigatório.")]
        public string IdPortfolio { get; set; }

        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        public string Nome { get; set; }
    }
}
