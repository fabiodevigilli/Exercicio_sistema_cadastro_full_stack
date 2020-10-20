using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstOne.Cadastros.Infra.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MongoDbContext _mongoDbContext;

        public UsuarioRepository()
        {
            _mongoDbContext = new MongoDbContext();
        }

        public void Adicionar(Usuario usuario)
        {
            _mongoDbContext.Usuarios.InsertOne(usuario);
        }
    }
}
