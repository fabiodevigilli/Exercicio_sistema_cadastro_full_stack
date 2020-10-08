using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Infra.Data.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        public void Adicionar(Pessoa pessoa)
        {
            
        }

        public IEnumerable<Pessoa> ObterTodos()
        {
            return new List<Pessoa>()
            {
                new Pessoa(Guid.NewGuid(), "Pessoa 1"),
                new Pessoa(Guid.NewGuid(), "Pessoa 2")
            };
        }
    }
}
