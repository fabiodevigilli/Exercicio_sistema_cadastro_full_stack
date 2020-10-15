using FirstOne.Cadastros.Domain.Commands;
using FluentValidation;

namespace FirstOne.Cadastros.Domain.Validations
{
    public class AtualizarPessoaCommandValidation : AbstractValidator<AtualizarPessoaCommand>
    {
        public AtualizarPessoaCommandValidation()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Por favor, informe o Id da Pessoa");
            RuleFor(p => p.Nome).NotEmpty().WithMessage("Por favor, informe o Nome da Pessoa");
        }
    }
}
