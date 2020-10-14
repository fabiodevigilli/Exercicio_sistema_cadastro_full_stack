using FirstOne.Cadastros.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Interfaces
{
    public interface IPessoaAppService
    {
        IEnumerable<PessoaViewModel> ObterTodos();
        Task Adicionar(PessoaViewModel pessoaViewModel);
        Task Atualizar(PessoaViewModel pessoaViewModel);
    }
}
