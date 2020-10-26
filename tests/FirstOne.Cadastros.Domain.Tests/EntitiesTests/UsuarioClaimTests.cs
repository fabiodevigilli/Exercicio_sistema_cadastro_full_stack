using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Enums;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FirstOne.Cadastros.Domain.Tests.EntitiesTests
{
    public class UsuarioClaimTests
    {
        [Fact(DisplayName = "DeveInstanciar_UsuarioClaim")]
        [Trait("Categoria", "Usuarioclaim")]
        public void DeveInstanciar_UsuarioClaim()
        {
            // Arrange
            var expected = new
            {
                Id = Guid.NewGuid(),
                UsuarioId = Guid.NewGuid(),
                Entidade = RotinaEntidades.Pessoa,
                Endpoint = "Adicionar, Atualizar, Remover"               
            };

            // Act
            var actual = new UsuarioClaim(expected.Id, expected.UsuarioId, expected.Entidade, expected.Endpoint);

            // Assert
            expected.Should().Equals(actual);
        }
    }
}
