using Domain.Notificacao;

namespace Application.Interfaces;
public interface INotificador
{
    bool TemNotificacao();
    List<Notificacao> ObterNotificacoes();
    void Handle(Notificacao notificacao);
}
