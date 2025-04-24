using static System.Net.Mime.MediaTypeNames;

namespace Domain.Notificacao;
public class Notificacao :  Application.Interface.INotificador
{
    public string Mensagem { get; }

    public Notificacao(string mensagem)
    {
        Mensagem = mensagem;
    }
}
