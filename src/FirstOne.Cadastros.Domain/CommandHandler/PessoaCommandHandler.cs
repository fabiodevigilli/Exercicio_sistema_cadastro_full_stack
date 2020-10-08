using FirstOne.Cadastros.Domain.Command;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.CommandHandler
{
    public class PessoaCommandHandler :
        IRequestHandler<AdicionarPessoaCommand, bool>
    {
        public Task<bool> Handle(AdicionarPessoaCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
