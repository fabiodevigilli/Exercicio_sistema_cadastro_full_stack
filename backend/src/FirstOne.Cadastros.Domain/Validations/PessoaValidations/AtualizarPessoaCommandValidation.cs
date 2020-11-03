using FirstOne.Cadastros.Domain.Commands;
using FluentValidation;

namespace FirstOne.Cadastros.Domain.Validations
{
    public class AtualizarPessoaCommandValidation : AbstractValidator<AtualizarPessoaCommand>
    {
        public AtualizarPessoaCommandValidation()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "Id"));
            RuleFor(p => p.Nome).NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "Nome"));
        }
    }
}
