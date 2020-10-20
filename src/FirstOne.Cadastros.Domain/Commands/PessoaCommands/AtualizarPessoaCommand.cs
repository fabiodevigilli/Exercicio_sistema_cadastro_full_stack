using FirstOne.Cadastros.Domain.Validations;
using FluentValidation.Results;
using System;

namespace FirstOne.Cadastros.Domain.Commands
{
    public class AtualizarPessoaCommand : Command
    {
        public Guid Id { get; }
        public string Nome { get; }

        public AtualizarPessoaCommand(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public override bool IsValid()
        {
            ValidationResult = new AtualizarPessoaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
