using FirstOne.Cadastros.Domain.Messaging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FirstOne.Cadastros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly DomainNotificationHandler _domainNotificationHandler;

        public BaseController(INotificationHandler<DomainNotification> notificationHandler)
        {
            _domainNotificationHandler = (DomainNotificationHandler)notificationHandler;
        }

        protected IActionResult CustomResponse()
        {
            if (_domainNotificationHandler.PossuiNotificacao())
            {
                return UnprocessableEntity(new
                {
                    errors = _domainNotificationHandler.ObterNotificacoes().Select(e => e.Value)
                });
            }
            return Ok();
        }
    }
}