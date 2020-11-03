using FirstOne.Cadastros.Domain.Commands;
using FluentValidation;

namespace FirstOne.Cadastros.Domain.Validations
{
    public class AdicionarPessoaCommandValidation : AbstractValidator<AdicionarPessoaCommand>
    {
        public AdicionarPessoaCommandValidation()
        {
            RuleFor(p => p.Nome).NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "Nome"));
        }
    }
}
