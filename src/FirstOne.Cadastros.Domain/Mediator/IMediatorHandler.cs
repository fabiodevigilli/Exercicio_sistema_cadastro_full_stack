using FirstOne.Cadastros.Domain.Command;
using FirstOne.Cadastros.Domain.Messaging;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.Mediator
{
    public interface IMediatorHandler
    {
        Task EnviarCommand<T>(T command) where T : AdicionarPessoaCommand;
        Task PublicarDomainNotification<T>(T notification) where T : DomainNotification;
    }
}
