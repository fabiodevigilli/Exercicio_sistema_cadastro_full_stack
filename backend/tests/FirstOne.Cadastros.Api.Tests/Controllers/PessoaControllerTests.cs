using FirstOne.Cadastros.Api.Controllers;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Messaging;
using Microsoft.AspNetCore.Mvc;
using Moq;
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

        [Fact(DisplayName = "Adicionar_Pessoas_Deve_Passar")]
        [Trait("Categoria", "Pessoacontroller")]
        public async Task Adicionar_Pessoas_Deve_Passar()
        {
            // Arrange
            var pessoa = new PessoaViewModel()
            {
                Nome = "Fulano 1"
            };

            _autoMocker.GetMock<DomainNotificationHandler>().Setup(e => e.PossuiNotificacao()).Returns(false);

            // Act
            var result = await _pessoaController.Adicionar(pessoa);

            // Assert
            var ok = result as OkResult;
            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
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

            var notifications = new List<DomainNotification>()
            {
                new DomainNotification("Por favor, informe o Nome da Pessoa")
            };

            _autoMocker.GetMock<DomainNotificationHandler>().Setup(e => e.PossuiNotificacao()).Returns(true);
            _autoMocker.GetMock<DomainNotificationHandler>().Setup(e => e.ObterNotificacoes()).Returns(notifications);

            // Act
            var result = await _pessoaController.Adicionar(pessoa);

            // Assert
            var erro = result as UnprocessableEntityObjectResult;
            Assert.NotNull(erro);
            Assert.Equal(422, erro.StatusCode);
        }

        [Fact(DisplayName = "Atualizar_Pessoa_Deve_Passar")]
        [Trait("Categoria", "Pessoacontroller")]
        public async Task Atualizar_Pessoa_Deve_Passar()
        {
            // Arrange
            var pessoa = new PessoaViewModel()
            {
                Id = Guid.NewGuid(),
                Nome = "Pessoa 1 Alterada"
            };

            _autoMocker.GetMock<DomainNotificationHandler>().Setup(e => e.PossuiNotificacao()).Returns(false);

            // Act
            var result = await _pessoaController.Atualizar(pessoa);

            // Assert
            _autoMocker.GetMock<IPessoaAppService>().Verify(e => e.Atualizar(It.IsAny<PessoaViewModel>()), Times.Once);
            _autoMocker.GetMock<DomainNotificationHandler>().Verify(e => e.PossuiNotificacao(), Times.Once);
            var ok = result as OkResult;
            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
        }

        [Fact(DisplayName = "Atualizar_Pessoa_Deve_Falhar")]
        [Trait("Categoria", "Pessoacontroller")]
        public async Task Atualizar_Pessoa_Deve_Falhar()
        {
            // Arrange
            var pessoa = new PessoaViewModel()
            {
                Id = Guid.NewGuid(),
                Nome = ""
            };

            var notifications = new List<DomainNotification>()
            {
                new DomainNotification("Por favor, informe o Nome da Pessoa")
            };

            _autoMocker.GetMock<DomainNotificationHandler>().Setup(e => e.PossuiNotificacao()).Returns(true);
            _autoMocker.GetMock<DomainNotificationHandler>().Setup(e => e.ObterNotificacoes()).Returns(notifications);

            // Act
            var result = await _pessoaController.Atualizar(pessoa);

            // Assert
            _autoMocker.GetMock<IPessoaAppService>().Verify(e => e.Atualizar(It.IsAny<PessoaViewModel>()), Times.Once);
            _autoMocker.GetMock<DomainNotificationHandler>().Verify(e => e.PossuiNotificacao(), Times.Once);
            _autoMocker.GetMock<DomainNotificationHandler>().Verify(e => e.ObterNotificacoes(), Times.Once);
            var erro = result as UnprocessableEntityObjectResult;
            Assert.NotNull(erro);
            Assert.Equal(422, erro.StatusCode);
        }

        [Fact(DisplayName = "Remover_Pessoa_Deve_Passar")]
        [Trait("Categoria", "Pessoacontroller")]
        public async Task Remover_Pessoa_Deve_Passar()
        {
            // Arrange
            _autoMocker.GetMock<DomainNotificationHandler>().Setup(e => e.PossuiNotificacao()).Returns(false);

            // Act
            var result = await _pessoaController.Remover(Guid.NewGuid());

            // Assert
            _autoMocker.GetMock<IPessoaAppService>().Verify(e => e.Remover(It.IsAny<Guid>()), Times.Once);
            _autoMocker.GetMock<DomainNotificationHandler>().Verify(e => e.PossuiNotificacao(), Times.Once);
            var ok = result as OkResult;
            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
        }

        [Fact(DisplayName = "Remover_Pessoa_Deve_Falhar")]
        [Trait("Categoria", "Pessoacontroller")]
        public async Task Remover_Pessoa_Deve_Falhar()
        {
            // Arrange
            var notifications = new List<DomainNotification>()
            {
                new DomainNotification("Por favor, informe o Id da Pessoa")
            };

            _autoMocker.GetMock<DomainNotificationHandler>().Setup(e => e.PossuiNotificacao()).Returns(true);
            _autoMocker.GetMock<DomainNotificationHandler>().Setup(e => e.ObterNotificacoes()).Returns(notifications);

            // Act
            var result = await _pessoaController.Remover(Guid.Empty);

            // Assert
            _autoMocker.GetMock<IPessoaAppService>().Verify(e => e.Remover(It.IsAny<Guid>()), Times.Once);
            _autoMocker.GetMock<DomainNotificationHandler>().Verify(e => e.PossuiNotificacao(), Times.Once);
            _autoMocker.GetMock<DomainNotificationHandler>().Verify(e => e.ObterNotificacoes(), Times.Once);
            var erro = result as UnprocessableEntityObjectResult;
            Assert.NotNull(erro);
            Assert.Equal(422, erro.StatusCode);
        }
    }
}
