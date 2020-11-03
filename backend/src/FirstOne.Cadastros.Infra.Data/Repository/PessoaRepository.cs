using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstOne.Cadastros.Infra.Data.Repository
{
    public class PessoaRepository : Repository, IPessoaRepository
    {
        public PessoaRepository(SQLServerContext context) : base(context) { }

        public void Adicionar(Pessoa pessoa)
        {
            _context.Pessoa.Add(pessoa);
        }

        public void Atualizar(Pessoa pessoa)
        {
            _context.Pessoa.Update(pessoa);
        }

        public void Remover(Guid id)
        {
            _context.Pessoa.Remove(ObterPorId(id));
        }

        public IEnumerable<Pessoa> ObterTodos()
        {
            return _context.Pessoa.AsNoTracking().ToList();
        }

        public Pessoa ObterPorId(Guid id)
        {
            return _context.Pessoa.AsNoTracking().FirstOrDefault(e => e.Id == id);
        }
    }
}
