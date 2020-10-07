using FirstOne.Cadastros.Api.Controllers;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using Xunit;

namespace FirstOne.Cadastros.Api.Tests.Controllers
{
    public class PessoaControllerTests
    {
        private readonly AutoMocker _autoMocker;
        private readonly PessoaController _pessoaController;

        public PessoaControllerTests()
        {
            _autoMocker = new AutoMocker();
            _pessoaController = _autoMocker.CreateInstance<PessoaController>();
        }

        [Fact(DisplayName = "ObterTodos_DeveRetornarPessoas")]
        [Trait("Categoria", "Pessoacontroller")]
        public void ObterTodos_DeveRetornarPessoas()
        {
            // Arrange
            var pessoas = new List<PessoaViewModel>()
            {
                new PessoaViewModel
                {
                    Id = Guid.NewGuid(),
                    Nome = "Pessoa Teste 1"
                }
            };
            _autoMocker.GetMock<IPessoaAppService>().Setup(e => e.ObterTodos()).Returns(pessoas);

            // Act
            var result = _pessoaController.ObterTodos();

            // Assert
            Assert.Single(result);
        }
    }
}
