using FirstOne.Cadastros.Api.Controllers;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Messaging;
using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            _pessoaController = new PessoaController(_autoMocker.GetMock<IPessoaAppService>().Object,
                                                     _autoMocker.GetMock<DomainNotificationHandler>().Object);
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
        public async Task Adicionar_Pessoas_Deve_Falhar()
        {
            // Arrange
            var pessoa = new PessoaViewModel()
            {
                Nome = ""
            };

            var norifications = new List<DomainNotification>()
            {
                new DomainNotification("Por favor, informe o Nome da Pessoa")
            };

            _autoMocker.GetMock<DomainNotificationHandler>().Setup(e => e.PossuiNotificacao()).Returns(true);
            _autoMocker.GetMock<DomainNotificationHandler>().Setup(e => e.ObterNotificacoes()).Returns(norifications);

            // Act
            var result = await _pessoaController.Adicionar(pessoa);

            // Assert
            var erro = result as UnprocessableEntityObjectResult;
            Assert.NotNull(erro);
            Assert.Equal(422, erro.StatusCode);
        }
    }
}
