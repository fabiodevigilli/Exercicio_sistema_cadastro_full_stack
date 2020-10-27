using FirstOne.Cadastros.Api.Config;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Messaging;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuarioController(IUsuarioAppService usuarioAppService, INotificationHandler<DomainNotification> notificationHandler) : base(notificationHandler)
        {
            _usuarioAppService = usuarioAppService;
        }

        [HttpPost]
        [ClaimsAuthorize("Usuario", "Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] UsuarioViewModel usuarioViewmodel)
        {
            await _usuarioAppService.Adicionar(usuarioViewmodel);

            return CustomResponse();
        }

        [HttpGet]
        [ClaimsAuthorize("Usuario", "ObterTodos")]
        public IEnumerable<UsuarioViewModel> ObterTodos()
        {
            return _usuarioAppService.ObterTodos();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            var token = _usuarioAppService.Login(loginViewModel.Email, loginViewModel.Senha);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }

        [HttpPost("claims")]
        [ClaimsAuthorize("Usuario", "Claims")]
        public async Task<IActionResult> AtualizarPermissoes([FromBody] UsuarioClaimViewmodel usuarioPermissoesViewmodel)
        {
            await _usuarioAppService.AtualizarClaims(usuarioPermissoesViewmodel);
            return Ok();
        }

        //[HttpGet("permissoes/{usuarioId}")]
        //public UsuarioClaimViewmodel ObterPermissoes(Guid usuarioId)
        //{
        //    return _usuarioAppService.ObterPermissoes(usuarioId);
        //}
    }
}