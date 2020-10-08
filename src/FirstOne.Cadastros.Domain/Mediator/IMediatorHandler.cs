using FirstOne.Cadastros.Domain.Command;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.Mediator
{
    public interface IMediatorHandler
    {
        Task EnviarCommand<T>(T command) where T : AdicionarPessoaCommand;
    }
}
