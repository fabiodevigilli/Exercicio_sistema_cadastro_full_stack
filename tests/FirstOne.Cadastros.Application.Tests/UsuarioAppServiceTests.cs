using FirstOne.Cadastros.Application.Services;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Commands.UsuarioCommands;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Domain.Messaging;
using FirstOne.Cadastros.Domain.Validations;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FirstOne.Cadastros.Application.Tests
{
    public class UsuarioAppServiceTests
    {
        private readonly AutoMocker _autoMocker;
        private readonly UsuarioAppService _usuarioAppService;

        public UsuarioAppServiceTests()
        {
            _autoMocker = new AutoMocker();
            _usuarioAppService = _autoMocker.CreateInstance<UsuarioAppService>();
        }

        [Fact(DisplayName = "Deve_Adicionar_Usuario")]
        [Trait("Categoria", "UsuarioAppService")]
        public async Task Deve_Adicionar_Usuario()
        {
            // Arrange
            var usuarioViewModel = new UsuarioViewModel()
            {
                Email = "fulano@hbsis.com.br",
                Senha = "1234",
                PessoaId = Guid.NewGuid()
            };

            // Act
            await _usuarioAppService.Adicionar(usuarioViewModel);

            // Assert
            _autoMocker.GetMock<IMediatorHandler>().Verify(e => e.PublicarDomainNotification(It.IsAny<DomainNotification>()), Times.Never);
            _autoMocker.GetMock<IMediatorHandler>().Verify(e => e.EnviarCommand(It.IsAny<AdicionarUsuarioCommand>()), Times.Once);
        }

        [Fact(DisplayName = "Nao_Deve_Adicionar_Usuario_Email_Vazio")]
        [Trait("Categoria", "UsuarioAppService")]
        public async Task Nao_Deve_Adicionar_Usuario_Email_Vazio()
        {
            // Arrange
            var usuarioViewModel = new UsuarioViewModel()
            {
                Email = "",
                Senha = "1234",
                PessoaId = Guid.NewGuid()
            };

            // Act
            await _usuarioAppService.Adicionar(usuarioViewModel);

            // Assert
            _autoMocker.GetMock<IMediatorHandler>().Verify(e => e.PublicarDomainNotification(
                It.Is<DomainNotification>(dn => dn.Value == string.Format(ValidationMessages.RequiredField, "Email"))), Times.Once);

            _autoMocker.GetMock<IMediatorHandler>().Verify(e => e.EnviarCommand(
                It.IsAny<AdicionarUsuarioCommand>()), Times.Never);
        }

        [Fact(DisplayName = "Nao_Deve_Adicionar_Usuario_Email_Invalido")]
        [Trait("Categoria", "UsuarioAppService")]
        public async Task Nao_Deve_Adicionar_Usuario_Email_Invalido()
        {
            // Arrange
            var usuarioViewModel = new UsuarioViewModel()
            {
                Email = "fulano",
                Senha = "1234",
                PessoaId = Guid.NewGuid()
            };

            // Act
            await _usuarioAppService.Adicionar(usuarioViewModel);

            // Assert
            _autoMocker.GetMock<IMediatorHandler>().Verify(e => e.PublicarDomainNotification(
                It.Is<DomainNotification>(dn => dn.Value == string.Format(ValidationMessages.RequiredField, "Email"))), Times.Once);

            _autoMocker.GetMock<IMediatorHandler>().Verify(e => e.EnviarCommand(
                It.IsAny<AdicionarUsuarioCommand>()), Times.Never);
        }
    }
}
