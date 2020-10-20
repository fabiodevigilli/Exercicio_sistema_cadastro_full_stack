using FirstOne.Cadastros.Domain.Validations.UsuarioValidations;
using System;

namespace FirstOne.Cadastros.Domain.Commands.UsuarioCommands
{
    public class AdicionarUsuarioCommand : Command
    {
        public string Email { get; }
        public string Senha { get; }
        public Guid PessoaId { get; }

        public AdicionarUsuarioCommand(string email, string senha, Guid pessoaId)
        {
            Email = email;
            Senha = senha;
            PessoaId = pessoaId;
        }

        public override bool IsValid()
        {
            ValidationResult = new AdicionarUsuarioCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
