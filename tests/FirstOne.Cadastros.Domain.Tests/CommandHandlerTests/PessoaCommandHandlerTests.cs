using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.CommandHandler;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Domain.Messaging;
using Moq;
using Moq.AutoMock;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System;

namespace FirstOne.Cadastros.Domain.Tests.CommandHandlerTests
{
    public class PessoaCommandHandlerTests
    {
        private readonly AutoMocker _autoMocker;
        private readonly PessoaCommandHandler _pessoaCommandHandler;

        public PessoaCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();
            _pessoaCommandHandler = _autoMocker.CreateInstance<PessoaCommandHandler>();
        }

        [Fact(DisplayName = "Adicionar deve executar com sucesso")]
        [Trait("Categoria", "PessoaCommandHandler")]
        public async Task Adicionar_Deve_Executar_Com_Sucesso()
        {
            // Arrange
            var adicionarPessoaCommand = new AdicionarPessoaCommand("Pessoa 1");

            // Act
            var result = await _pessoaCommandHandler.Handle(adicionarPessoaCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
            _autoMocker.GetMock<IPessoaRepository>().Verify(e => e.Adicionar(It.IsAny<Pessoa>()), Times.Once);
        }

        [Fact(DisplayName = "Handle_AdicionarProdutoCommand_DevePublicarDomainNotification_QuandoNomeNaoInformado")]
        [Trait("Categoria", "PessoaCommandHandler")]
        public async Task Handle_AdicionarProdutoCommand_DevePublicarDomainNotification_QuandoNomeNaoInformado()
        {
            // Arrange
            var adicionarPessoaCommand = new AdicionarPessoaCommand("");

            // Act
            var result = await _pessoaCommandHandler.Handle(adicionarPessoaCommand, CancellationToken.None);

            // Assert
            Assert.False(result);
            _autoMocker.GetMock<IMediatorHandler>().Verify(e => e.PublicarDomainNotification(
                It.Is<DomainNotification>(dn => dn.Value == "Por favor, informe o Nome da Pessoa")), Times.Once);
            _autoMocker.GetMock<IPessoaRepository>().Verify(e => e.Adicionar(It.IsAny<Pessoa>()), Times.Never);
        }

        [Fact(DisplayName = "Atualizar deve executar com sucesso")]
        [Trait("Categoria", "PessoaCommandHandler")]
        public async Task Atualizar_Deve_Executar_Com_Sucesso()
        {
            // Arrange
            var command = new AtualizarPessoaCommand(Guid.NewGuid(),"Pessoa 1");

            // Act
            var result = await _pessoaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IPessoaRepository>().Verify(e => e.Atualizar(It.IsAny<Pessoa>()), Times.Once);
        }

        [Fact(DisplayName = "Handle_AtualizarProdutoCommand_DevePublicarDomainNotification_QuandoNomeNaoInformado")]
        [Trait("Categoria", "PessoaCommandHandler")]
        public async Task Handle_AtualizarProdutoCommand_DevePublicarDomainNotification_QuandoNomeNaoInformado()
        {
            // Arrange
            var command = new AtualizarPessoaCommand(Guid.NewGuid(),"");

            // Act
            var result = await _pessoaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _autoMocker.GetMock<IMediatorHandler>().Verify(e => e.PublicarDomainNotification(
                It.Is<DomainNotification>(dn => dn.Value == "Por favor, informe o Nome da Pessoa")), Times.Once);
            _autoMocker.GetMock<IPessoaRepository>().Verify(e => e.Atualizar(It.IsAny<Pessoa>()), Times.Never);
        }

        [Fact(DisplayName = "Handle_AtualizarProdutoCommand_DevePublicarDomainNotification_QuandoIdNaoInformado")]
        [Trait("Categoria", "PessoaCommandHandler")]
        public async Task Handle_AtualizarProdutoCommand_DevePublicarDomainNotification_QuandoIdNaoInformado()
        {
            // Arrange
            var command = new AtualizarPessoaCommand(Guid.Empty, "Pessoa 1");

            // Act
            var result = await _pessoaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _autoMocker.GetMock<IMediatorHandler>().Verify(e => e.PublicarDomainNotification(
                It.Is<DomainNotification>(dn => dn.Value == "Por favor, informe o Id da Pessoa")), Times.Once);
            _autoMocker.GetMock<IPessoaRepository>().Verify(e => e.Atualizar(It.IsAny<Pessoa>()), Times.Never);
        }

        [Fact(DisplayName = "Remover deve executar com sucesso")]
        [Trait("Categoria", "PessoaCommandHandler")]
        public async Task Remover_Deve_Executar_Com_Sucesso()
        {
            // Arrange
            var command = new RemoverPessoaCommand(Guid.NewGuid());

            // Act
            var result = await _pessoaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IPessoaRepository>().Verify(e => e.Remover(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "Handle_RemoverProdutoCommand_DevePublicarDomainNotification_QuandoIdNaoInformado")]
        [Trait("Categoria", "PessoaCommandHandler")]
        public async Task Handle_RemoverProdutoCommand_DevePublicarDomainNotification_QuandoIdNaoInformado()
        {
            // Arrange
            var command = new RemoverPessoaCommand(Guid.Empty);

            // Act
            var result = await _pessoaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _autoMocker.GetMock<IMediatorHandler>().Verify(e => e.PublicarDomainNotification(
                It.Is<DomainNotification>(dn => dn.Value == "Por favor, informe o Id da Pessoa")), Times.Once);
            _autoMocker.GetMock<IPessoaRepository>().Verify(e => e.Remover(It.IsAny<Guid>()), Times.Never);
        }
    }
}
