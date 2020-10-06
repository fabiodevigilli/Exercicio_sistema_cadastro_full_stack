using FirstOne.Cadastros.Application.ViewModels;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Application.Interfaces
{
    public interface IPessoaAppService
    {
        IEnumerable<PessoaViewModel> ObterTodos();
    }
}
