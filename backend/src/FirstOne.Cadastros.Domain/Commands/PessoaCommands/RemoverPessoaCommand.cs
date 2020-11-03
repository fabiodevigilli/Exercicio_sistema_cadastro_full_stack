using FirstOne.Cadastros.Domain.Validations;
using System;

namespace FirstOne.Cadastros.Domain.Commands
{
    public class RemoverPessoaCommand : Command
    {
        public Guid Id { get; }

        public RemoverPessoaCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoverPessoaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
