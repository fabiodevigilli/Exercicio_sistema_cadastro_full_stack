using FirstOne.Cadastros.Domain.Commands.UsuarioCommands;
using FluentValidation;

namespace FirstOne.Cadastros.Domain.Validations.UsuarioValidations
{
    public class AdicionarUsuarioCommandValidation : AbstractValidator<AdicionarUsuarioCommand>
    {
        public AdicionarUsuarioCommandValidation()
        {
            RuleFor(p => p.Email).EmailAddress().WithMessage(string.Format(ValidationMessages.RequiredField, "Email"));
            RuleFor(p => p.Senha).NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "Senha"));
            RuleFor(p => p.PessoaId).NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "PessoaId"));
        }
    }
}
