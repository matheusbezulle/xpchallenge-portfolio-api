using System.Diagnostics;

namespace XpChallenge.Portfolio.Api.Responses
{
    public class ResponseBase(bool erro = false)
    {
        public bool Erro { get; set; } = erro;
        public IEnumerable<string> Mensagens { get; set; } = [];
        public string CorrelationId { get; set; } = Activity.Current?.Id ?? string.Empty;
    }
}
