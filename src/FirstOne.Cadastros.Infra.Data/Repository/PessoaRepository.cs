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
    public class PessoaRepository : IPessoaRepository
    {
        private readonly SQLServerContext _context;

        public PessoaRepository(SQLServerContext context)
        {
            _context = context;
        }

        public void Adicionar(Pessoa pessoa)
        {
            _context.Pessoa.Add(pessoa);
            _context.SaveChanges();
        }

        public void Atualizar(Pessoa pessoa)
        {
            _context.Pessoa.Update(pessoa);
            _context.SaveChanges();
        }

        public void Remover(Guid id)
        {
            _context.Pessoa.Remove(ObterPorId(id));
            _context.SaveChanges();
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
