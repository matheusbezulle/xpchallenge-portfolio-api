namespace XpChallenge.Portfolio.Application.DataTransfer
{
    public class TotalizadorDto(string nome, double porcentagem)
    {
        public string Nome { get; set; } = nome;
        public double Porcentagem { get; set; } = porcentagem;
    }
}