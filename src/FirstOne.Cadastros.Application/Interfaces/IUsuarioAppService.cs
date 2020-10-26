using FirstOne.Cadastros.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        Task Adicionar(UsuarioViewModel usuarioViewModel);
        string Login(string email, string password);
        IEnumerable<UsuarioViewModel> ObterTodos();
        void AdicionarClaims(UsuarioClaimViewmodel usuarioPermissoesViewmodel);
        // UsuarioPermissoesViewmodel ObterPermissoes(Guid usuarioid);
    }
}
