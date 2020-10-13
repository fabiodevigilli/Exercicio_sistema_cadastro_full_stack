using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Messaging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaAppService _pessoaAppService;
        private readonly DomainNotificationHandler _domainNotificationHandler;

        public PessoaController(IPessoaAppService pessoaAppService, INotificationHandler<DomainNotification> notificationHandler)
        {
            _pessoaAppService = pessoaAppService;
            _domainNotificationHandler = (DomainNotificationHandler)notificationHandler;
        }

        [HttpGet]
        public IEnumerable<PessoaViewModel> ObterTodos()
        {
            return _pessoaAppService.ObterTodos();
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] PessoaViewModel pessoaViewmodel)
        {
            await _pessoaAppService.Adicionar(pessoaViewmodel);

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