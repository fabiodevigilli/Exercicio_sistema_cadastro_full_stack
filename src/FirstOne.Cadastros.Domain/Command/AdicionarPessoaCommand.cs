using FirstOne.Cadastros.Domain.Validations;
using FluentValidation.Results;
using MediatR;

namespace FirstOne.Cadastros.Domain.Command
{
    public class AdicionarPessoaCommand : IRequest<bool>
    {
        public string Nome { get; }
        public ValidationResult ValidationResult { get; private set; }

        public AdicionarPessoaCommand(string nome)
        {
            Nome = nome;
        }

        public bool IsValid()
        {
            ValidationResult = new AdicionarPessoaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
