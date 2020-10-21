using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Messaging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuarioController(IUsuarioAppService usuarioAppService, INotificationHandler<DomainNotification> notificationHandler) : base(notificationHandler)
        {
            _usuarioAppService = usuarioAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] UsuarioViewModel usuarioViewmodel)
        {
            await _usuarioAppService.Adicionar(usuarioViewmodel);

            return CustomResponse();
        }

        [HttpGet]
        public IEnumerable<UsuarioViewModel> ObterTodos()
        {
            return _usuarioAppService.ObterTodos();
        }
    }
}