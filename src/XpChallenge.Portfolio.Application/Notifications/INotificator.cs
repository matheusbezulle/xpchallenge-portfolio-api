namespace XpChallenge.Portfolio.Application.Notifications
{
    public interface INotificator
    {
        List<string> ObterMensagens();
        bool PossuiMensagens();
        bool PossuiErros();
        bool PossuiErrosNegocio();
        bool PossuiErrosAplicacao();
        void AdicionarErroNegocio(string mensagem);
        void AdicionarErroAplicacao(string mensagem);
    }
}
