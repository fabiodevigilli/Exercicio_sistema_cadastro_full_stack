using FirstOne.Cadastros.Api.Controllers;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Messaging;
using FirstOne.Cadastros.Domain.Validations;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FirstOne.Cadastros.Api.Tests.Controllers
{
    public class UsuarioControllerTests
    {
        private readonly AutoMocker _autoMocker;
        private readonly UsuarioController _usuarioController;

        public UsuarioControllerTests()
        {
            _autoMocker = new AutoMocker();
            _usuarioController = new UsuarioController(_autoMocker.GetMock<IUsuarioAppService>().Object,
                                                     _autoMocker.GetMock<DomainNotificationHandler>().Object);
        }

        [Fact(DisplayName = "Adicionar_Usuario_Deve_Passar")]
        [Trait("Categoria", "UsuarioController")]
        public async Task Adicionar_Usuario_Deve_Passar()
        {
            // Arrange
            var usuario = new UsuarioViewModel()
            {
                Email = "fulano@hbsis.com.br",
                Senha = "1234",
                PessoaId = Guid.NewGuid()
            };

            _autoMocker.GetMock<DomainNotificationHandler>().Setup(e => e.PossuiNotificacao()).Returns(false);

            // Act
            var result = await _usuarioController.Adicionar(usuario);

            // Assert
            _autoMocker.GetMock<IUsuarioAppService>().Verify(e => e.Adicionar(It.IsAny<UsuarioViewModel>()), Times.Once);
            _autoMocker.GetMock<DomainNotificationHandler>().Verify(e => e.PossuiNotificacao(), Times.Once);
            var ok = result as OkResult;
            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
        }

        [Fact(DisplayName = "Adicionar_Usuario_Deve_Falhar")]
        [Trait("Categoria", "UsuarioController")]
        public async Task Adicionar_Usuario_Deve_Falhar()
        {
            // Arrange
            var usuario = new UsuarioViewModel()
            {
                Email = "",
                Senha = "1234",
                PessoaId = Guid.NewGuid()
            };

            var notifications = new List<DomainNotification>()
            {
                new DomainNotification(string.Format(ValidationMessages.RequiredField,"Email"))
            };

            _autoMocker.GetMock<DomainNotificationHandler>().Setup(e => e.PossuiNotificacao()).Returns(true);
            _autoMocker.GetMock<DomainNotificationHandler>().Setup(e => e.ObterNotificacoes()).Returns(notifications);

            // Act
            var result = await _usuarioController.Adicionar(usuario);

            // Assert
            _autoMocker.GetMock<IUsuarioAppService>().Verify(e => e.Adicionar(It.IsAny<UsuarioViewModel>()), Times.Once);
            _autoMocker.GetMock<DomainNotificationHandler>().Verify(e => e.PossuiNotificacao(), Times.Once);
            _autoMocker.GetMock<DomainNotificationHandler>().Verify(e => e.ObterNotificacoes(), Times.Once);

            var erro = result as UnprocessableEntityObjectResult;
            Assert.NotNull(erro);
            Assert.Equal(422, erro.StatusCode);
        }
    }
}
