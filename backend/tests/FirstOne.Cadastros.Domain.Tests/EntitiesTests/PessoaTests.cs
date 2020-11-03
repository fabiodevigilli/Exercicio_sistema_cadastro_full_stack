using FirstOne.Cadastros.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace FirstOne.Cadastros.Domain.Tests
{
    public class PessoaTests
    {
        [Fact(DisplayName = "Deve criar uma entidade Pessoa")]
        [Trait("Categoria", "Pessoa")]
        public void deve_criar_entidade_pessoa()
        {
            // Arrange
            var pessoaEsperada = new { Id = Guid.NewGuid(), Nome = "Pessoa Teste 1" };

            // Act
            var pessoa = new Pessoa(pessoaEsperada.Id, pessoaEsperada.Nome);

            // Assert
            pessoaEsperada.Should().Equals(pessoa);
        }
    }
}
