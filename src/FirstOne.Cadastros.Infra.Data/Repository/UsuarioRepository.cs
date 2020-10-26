using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Enums;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FirstOne.Cadastros.Infra.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SQLServerContext _context;

        public UsuarioRepository(SQLServerContext context)
        {
            _context = context;
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public IEnumerable<Usuario> ObterTodos()
        {
            return _context.Usuario.AsNoTracking()
                .Include(x => x.Pessoa)
                .ToList();
        }

        public IEnumerable<Usuario> Search(Expression<Func<Usuario, bool>> predicate)
        {
            return _context.Usuario.AsNoTracking().Include(x => x.UsuarioClaims).Where(predicate).ToList();
        }

        public void AdicionarClaim(UsuarioClaim usuarioClaim)
        {
            _context.UsuarioClaim.Add(usuarioClaim);
            _context.SaveChanges();
        }

        public void RemoverClaim(UsuarioClaim usuarioClaim)
        {
            _context.UsuarioClaim.Remove(usuarioClaim);
            _context.SaveChanges();
        }

        //public IEnumerable<UsuarioClaim> ObterPermissoes(Guid usuarioid)
        //{
        //    return _context.PermissoesUsuario.AsNoTracking().Where(x => x.UsuarioId == usuarioid).ToList();
        //}
    }
}
