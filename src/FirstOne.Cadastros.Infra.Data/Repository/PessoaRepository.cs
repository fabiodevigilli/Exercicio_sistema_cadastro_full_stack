using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Infra.Data.Context;
using MongoDB.Driver;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Infra.Data.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly MongoDbContext _mongoDbContext;

        public PessoaRepository()
        {
            _mongoDbContext = new MongoDbContext();
        }

        public void Adicionar(Pessoa pessoa)
        {
            _mongoDbContext.Pessoas.InsertOne(pessoa);
        }

        public IEnumerable<Pessoa> ObterTodos()
        {
            return _mongoDbContext.Pessoas.Find(m => true).ToList();
        }
    }
}
