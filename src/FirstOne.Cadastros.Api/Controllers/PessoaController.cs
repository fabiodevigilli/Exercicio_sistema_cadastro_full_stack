using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstOne.Cadastros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaAppService _pessoaAppService;

        public PessoaController(IPessoaAppService pessoaAppService)
        {
            _pessoaAppService = pessoaAppService;
        }

        [HttpGet]
        public IEnumerable<PessoaViewModel> ObterTodos()
        {
            return _pessoaAppService.ObterTodos();
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] PessoaViewModel pessoaViewmodel)
        {
            var result =_pessoaAppService.Adicionar(pessoaViewmodel);

            if (result.IsValid)
            {
                return Ok();
            }
            return UnprocessableEntity(new
            {
                errors = result.Errors.Select(e => e.ErrorMessage)
            });
        }
    }
}