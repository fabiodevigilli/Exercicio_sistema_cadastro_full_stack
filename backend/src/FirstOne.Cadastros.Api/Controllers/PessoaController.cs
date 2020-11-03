using FirstOne.Cadastros.Api.Config;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Messaging;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PessoaController : BaseController
    {
        private readonly IPessoaAppService _pessoaAppService;

        public PessoaController(IPessoaAppService pessoaAppService, INotificationHandler<DomainNotification> notificationHandler) : base(notificationHandler)
        {
            _pessoaAppService = pessoaAppService;
        }

        [HttpGet]
        //[ClaimsAuthorize("Pessoa", "ObterTodos")]
        [ClaimsAuthorize("Role", "Gestor,Borracheiro")]
        public IEnumerable<PessoaViewModel> ObterTodos()
        {
            return _pessoaAppService.ObterTodos();
        }

        [HttpGet("{id}")]
        //[ClaimsAuthorize("Pessoa", "ObterPorId")]
        [Authorize(Roles = "Gestor")]
        public PessoaViewModel ObterPorId(Guid id)
        {
            return _pessoaAppService.ObterPorId(id);
        }

        [HttpPost]
        //[ClaimsAuthorize("Pessoa", "Adicionar")]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Adicionar([FromBody] PessoaViewModel pessoaViewmodel)
        {
            await _pessoaAppService.Adicionar(pessoaViewmodel);

            return CustomResponse();
        }

        [HttpPut]
        //[ClaimsAuthorize("Pessoa", "Atualizar")]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Atualizar([FromBody] PessoaViewModel pessoaViewmodel)
        {
            await _pessoaAppService.Atualizar(pessoaViewmodel);

            return CustomResponse();
        }

        [HttpDelete("{id}")]
        //[ClaimsAuthorize("Pessoa", "Remover")]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Remover(Guid id)
        {
            await _pessoaAppService.Remover(id);

            return CustomResponse();
        }
    }
}