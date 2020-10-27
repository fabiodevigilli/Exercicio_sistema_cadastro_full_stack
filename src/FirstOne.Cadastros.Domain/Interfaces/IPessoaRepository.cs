using FirstOne.Cadastros.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Domain.Interfaces
{
    public interface IPessoaRepository : IRepository
    {
        IEnumerable<Pessoa> ObterTodos();
        Pessoa ObterPorId(Guid id);
        void Adicionar(Pessoa pessoa);
        void Atualizar(Pessoa pessoa);
        void Remover(Guid id);
    }
}
