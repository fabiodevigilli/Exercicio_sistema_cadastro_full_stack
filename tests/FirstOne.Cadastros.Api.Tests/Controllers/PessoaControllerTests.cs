using FirstOne.Cadastros.Api.Controllers;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Command;
using Microsoft.AspNetCore.Mvc;
using Moq;
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

        [Fact(DisplayName = "Adicionar_Pessoas_Deve_Falhar")]
        [Trait("Categoria", "Pessoacontroller")]
        public void Adicionar_Pessoas_Deve_Falhar()
        {
            // Arrange
            var pessoa = new PessoaViewModel()
            {
                Nome = ""                
            };

            var command = new AdicionarPessoaCommand(pessoa.Nome);
            command.IsValid();

            _autoMocker.GetMock<IPessoaAppService>().Setup(e => e.Adicionar(It.IsAny<PessoaViewModel>())).Returns(command.ValidationResult);

            // Act
            var result = _pessoaController.Adicionar(pessoa);

            // Assert
            var erro = result as UnprocessableEntityObjectResult;
            Assert.NotNull(erro);
            Assert.Equal(422, erro.StatusCode);
        }
    }
}
