using FirstOne.Cadastros.Domain.Enums;
using System;

namespace FirstOne.Cadastros.Domain.Entities
{
    public class UsuarioClaim : EntidadeBase
    {
        public Guid UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }
        public RotinaEntidades Entidade { get; private set; }
        public string Endpoint { get; private set; }

        public UsuarioClaim(Guid id, Guid usuarioid, RotinaEntidades entidade, string endpoint)
        {
            Id = id;
            UsuarioId = usuarioid;
            Entidade = entidade;
            Endpoint = endpoint;
        }

        protected UsuarioClaim() { }
    }
}
