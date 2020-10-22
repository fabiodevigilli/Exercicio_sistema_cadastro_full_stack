using FirstOne.Cadastros.Domain.Enums;
using System;

namespace FirstOne.Cadastros.Domain.Entities
{
    public class PermissoesUsuario : EntidadeBase
    {
        public Guid UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }
        public RotinaEntidades RotinaEntidades { get; private set; }
        public string Permissao { get; private set; }

        public PermissoesUsuario(Guid id, Guid usuarioid, RotinaEntidades rotinaEntidades, string permissao)
        {
            Id = id;
            UsuarioId = usuarioid;
            RotinaEntidades = rotinaEntidades;
            Permissao = permissao;
        }

        protected PermissoesUsuario() { }
    }
}
