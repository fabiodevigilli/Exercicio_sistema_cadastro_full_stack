using FirstOne.Cadastros.Domain.Validations.UsuarioValidations;
using System;

namespace FirstOne.Cadastros.Domain.Commands.UsuarioCommands
{
    public class AdicionarUsuarioCommand : Command
    {
        public string Email { get; }
        public string Senha { get; }
        public Guid PessoaId { get; }
        public string Role { get; }

        public AdicionarUsuarioCommand(string email, string senha, Guid pessoaId, string role)
        {
            Email = email;
            Senha = senha;
            PessoaId = pessoaId;
            Role = role;
        }

        public override bool IsValid()
        {
            ValidationResult = new AdicionarUsuarioCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
