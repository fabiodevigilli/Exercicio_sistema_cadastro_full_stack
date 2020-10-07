﻿using FirstOne.Cadastros.Application.ViewModels;
using FluentValidation.Results;
using System.Collections.Generic;


namespace FirstOne.Cadastros.Application.Interfaces
{
    public interface IPessoaAppService
    {
        IEnumerable<PessoaViewModel> ObterTodos();
        ValidationResult Adicionar(PessoaViewModel pessoaViewModel);
    }
}
