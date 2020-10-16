using FirstOne.Cadastros.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Interfaces
{
    public interface IPessoaAppService
    {
        IEnumerable<PessoaViewModel> ObterTodos();
        Task Adicionar(PessoaViewModel pessoaViewModel);
        Task Atualizar(PessoaViewModel pessoaViewModel);
        Task Remover(Guid id);
    }
}
