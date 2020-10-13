using FirstOne.Cadastros.Domain.Command;
using FirstOne.Cadastros.Domain.CommandHandler;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
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
    }
}
