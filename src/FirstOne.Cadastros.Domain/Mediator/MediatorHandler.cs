using FirstOne.Cadastros.Domain.Command;
using FirstOne.Cadastros.Domain.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task EnviarCommand<T>(T command) where T : AdicionarPessoaCommand
        {
            await _mediator.Send(command);
        }

        public async Task PublicarDomainNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }
    }
}
