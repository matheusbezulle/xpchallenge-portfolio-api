using System.ComponentModel.DataAnnotations;

namespace XpChallenge.Portfolio.Api.Requests
{
    public class IncluirProdutoFinanceiroRequest
    {
        [Required(ErrorMessage = "O campo 'IdPortfolio' é obrigatório.")]
        public string IdPortfolio { get; set; }

        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo 'IdCategoria' é obrigatório.")]
        [Range(1, 6, ErrorMessage = "O campo 'IdCategoria' deve ser preenchido com valores de 1 a 6.")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "O campo 'Peso' é obrigatório.")]
        [Range(0, int.MaxValue, ErrorMessage = "O campo 'Peso' deve ser maior que zero.")]
        public int Peso { get; set; }

        [Required(ErrorMessage = "O campo 'DataVencimento' é obrigatório.")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo 'DataVencimento' deve ser preenchido no formato dd/MM/yyyy.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataVencimento { get; set; }
    }
}
