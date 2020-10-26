using FirstOne.Cadastros.Domain.Enums;
using System;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Application.ViewModels
{
    public class UsuarioClaimViewmodel
    {
        public Guid UsuarioId { get; set; }
        public IEnumerable<ClaimViewModel> UsuarioClaims { get; set; }
    }

    public class ClaimViewModel
    {
        public RotinaEntidades Entidade { get; set; }
        public string Endpoint { get; set; }
    }
}

