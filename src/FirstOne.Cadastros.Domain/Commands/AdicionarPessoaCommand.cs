using FirstOne.Cadastros.Domain.Validations;
using FluentValidation.Results;

namespace FirstOne.Cadastros.Domain.Commands
{
    public class AdicionarPessoaCommand : Command
    {
        public string Nome { get; }

        public AdicionarPessoaCommand(string nome)
        {
            Nome = nome;
        }

        public override bool IsValid()
        {
            ValidationResult = new AdicionarPessoaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
