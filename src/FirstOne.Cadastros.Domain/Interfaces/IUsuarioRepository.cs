using FirstOne.Cadastros.Domain.Entities;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);
        IEnumerable<Usuario> ObterTodos();
    }
}
