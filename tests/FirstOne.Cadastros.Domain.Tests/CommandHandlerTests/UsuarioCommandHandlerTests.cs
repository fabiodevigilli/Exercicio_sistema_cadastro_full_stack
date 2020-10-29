using FirstOne.Cadastros.Domain.CommandHandler;
using FirstOne.Cadastros.Domain.Commands.UsuarioCommands;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FirstOne.Cadastros.Domain.Tests.CommandHandlerTests
{
    public class UsuarioCommandHandlerTests
    {
        private readonly AutoMocker _autoMocker;
        private readonly UsuarioCommandHandler _usuarioCommandHandler;

        public UsuarioCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();
            _usuarioCommandHandler = _autoMocker.CreateInstance<UsuarioCommandHandler>();
        }

        [Fact(DisplayName = "Hanlde_AddUsuarioCommand_DeveSalvarUsuario")]
        [Trait("Categoria", "UsuarioCommandHandler")]
        public async Task Adicionar_Deve_Executar_Com_Sucesso()
        {
            // Arrange
            var command = new AdicionarUsuarioCommand("fulano@hbsis.com.br", "1234", Guid.NewGuid());

            _autoMocker.GetMock<IUsuarioRepository>()
                .Setup(e => e.UnitOfWork)
                .Returns(_autoMocker.GetMock<IUnitOfWork>().Object);

            // Act
            await _usuarioCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IUsuarioRepository>().Verify(e => e.Adicionar(It.IsAny<Usuario>()), Times.Once);
        }
    }
}
