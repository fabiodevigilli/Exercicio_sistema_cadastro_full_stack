using FirstOne.Cadastros.Application.ViewModels;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Interfaces
{
    public interface IPessoaAppService
    {
        IEnumerable<PessoaViewModel> ObterTodos();
        Task<ValidationResult> Adicionar(PessoaViewModel pessoaViewModel);
    }
}
