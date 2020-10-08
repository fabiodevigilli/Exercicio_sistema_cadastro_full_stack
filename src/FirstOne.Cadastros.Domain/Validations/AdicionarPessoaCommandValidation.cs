using FirstOne.Cadastros.Domain.Command;
using FluentValidation;

namespace FirstOne.Cadastros.Domain.Validations
{
    public class AdicionarPessoaCommandValidation : AbstractValidator<AdicionarPessoaCommand>
    {
        public AdicionarPessoaCommandValidation()
        {
            RuleFor(p => p.Nome).NotEmpty().WithMessage("Por favor, informe o Nome da Pessoa");
        }
    }
}
