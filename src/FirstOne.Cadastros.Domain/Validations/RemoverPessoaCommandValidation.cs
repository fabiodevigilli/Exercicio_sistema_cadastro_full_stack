using FirstOne.Cadastros.Domain.Commands;
using FluentValidation;

namespace FirstOne.Cadastros.Domain.Validations
{
    public class RemoverPessoaCommandValidation : AbstractValidator<RemoverPessoaCommand>
    {
        public RemoverPessoaCommandValidation()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Por favor, informe o Id da Pessoa");
        }
    }
}
