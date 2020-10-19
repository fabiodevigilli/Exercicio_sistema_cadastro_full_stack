using FirstOne.Cadastros.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace FirstOne.Cadastros.Domain.Tests.EntitiesTests
{
    public class UsuarioTests
    {
        [Fact(DisplayName = "Deve criar uma entidade Usuario")]
        [Trait("Categoria", "Usuario")]
        public void deve_criar_entidade_usuario()
        {
            // Arrange
            var usuarioEsperado = new { Id = Guid.NewGuid(), Email = "usuario@hbsis.com.br", Senha = "admin", PessoaId = Guid.NewGuid() };

            // Act
            var usuario = new Usuario(usuarioEsperado.Id, usuarioEsperado.Email, usuarioEsperado.Senha, usuarioEsperado.PessoaId);

            // Assert
            usuarioEsperado.Should().Equals(usuario);
        }
    }
}
