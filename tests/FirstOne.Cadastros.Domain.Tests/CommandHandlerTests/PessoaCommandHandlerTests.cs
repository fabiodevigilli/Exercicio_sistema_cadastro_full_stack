using FirstOne.Cadastros.Domain.Command;
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
    }
}
