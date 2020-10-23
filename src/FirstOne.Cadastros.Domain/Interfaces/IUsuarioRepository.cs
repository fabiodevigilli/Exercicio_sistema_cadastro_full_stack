using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FirstOne.Cadastros.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);
        IEnumerable<Usuario> Search(Expression<Func<Usuario, bool>> predicate);
        IEnumerable<Usuario> ObterTodos();
        void AdicionarPermissoes(Guid usuarioId, RotinaEntidades rotinas, string values);
        IEnumerable<PermissoesUsuario> ObterPermissoes(Guid usuarioid);
    }
}
