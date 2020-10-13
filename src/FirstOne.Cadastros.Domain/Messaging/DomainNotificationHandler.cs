using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.Messaging
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> ObterNotificacoes()
        {
            return _notifications;
        }

        public virtual bool PossuiNotificacao()
        {
            return _notifications.Count() > 0;
        }
    }
}
