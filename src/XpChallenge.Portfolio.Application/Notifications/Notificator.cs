
namespace XpChallenge.Portfolio.Application.Notifications
{
    public class Notificator : INotificator
    {
        public List<string> Mensagens { get; set; } = [];
        public bool ErroNegocio { get; private set; }
        public bool ErroAplicacao { get; private set; }

        public List<string> ObterMensagens()
        {
            return Mensagens;
        }

        public bool PossuiMensagens()
        {
            return Mensagens.Count != 0;
        }

        public bool PossuiErros()
        {
            return ErroNegocio || ErroAplicacao;
        }

        public bool PossuiErrosNegocio()
        {
            return ErroNegocio;
        }

        public bool PossuiErrosAplicacao()
        {
            return ErroAplicacao;
        }

        public void AdicionarErroNegocio(string mensagem)
        {
            Mensagens.Add(mensagem);
            ErroNegocio = true;
        }

        public void AdicionarErroAplicacao(string mensagem)
        {
            Mensagens.Add(mensagem);
            ErroAplicacao = true;
        }
    }
}
