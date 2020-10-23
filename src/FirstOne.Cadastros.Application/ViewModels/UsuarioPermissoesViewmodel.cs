using FirstOne.Cadastros.Domain.Enums;
using System;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Application.ViewModels
{
    public class UsuarioPermissoesViewmodel
    {
        public Guid UsuarioId { get; set; }
        public IEnumerable<PermissoesViewModel> Permissoes { get; set; }
    }

    public class PermissoesViewModel
    {
        public RotinaEntidades Rotinas { get; set; }
        public string[] Values { get; set; }
    }
}

