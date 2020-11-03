using FirstOne.Cadastros.Domain.CommandHandler;
using FirstOne.Cadastros.Domain.Commands;
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

            _autoMocker.GetMock<IPessoaRepository>()
                .Setup(e => e.UnitOfWork)
                .Returns(_autoMocker.GetMock<IUnitOfWork>().Object);

            // Act
            var result = await _pessoaCommandHandler.Handle(adicionarPessoaCommand, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IPessoaRepository>().Verify(e => e.Adicionar(It.IsAny<Pessoa>()), Times.Once);
        }

        [Fact(DisplayName = "Atualizar deve executar com sucesso")]
        [Trait("Categoria", "PessoaCommandHandler")]
        public async Task Atualizar_Deve_Executar_Com_Sucesso()
        {
            // Arrange
            var command = new AtualizarPessoaCommand(Guid.NewGuid(), "Pessoa 1");

            _autoMocker.GetMock<IPessoaRepository>()
                .Setup(e => e.UnitOfWork)
                .Returns(_autoMocker.GetMock<IUnitOfWork>().Object);

            // Act
            var result = await _pessoaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IPessoaRepository>().Verify(e => e.Atualizar(It.IsAny<Pessoa>()), Times.Once);
        }

        [Fact(DisplayName = "Remover deve executar com sucesso")]
        [Trait("Categoria", "PessoaCommandHandler")]
        public async Task Remover_Deve_Executar_Com_Sucesso()
        {
            // Arrange
            var command = new RemoverPessoaCommand(Guid.NewGuid());

            _autoMocker.GetMock<IPessoaRepository>()
                .Setup(e => e.UnitOfWork)
                .Returns(_autoMocker.GetMock<IUnitOfWork>().Object);

            // Act
            var result = await _pessoaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IPessoaRepository>().Verify(e => e.Remover(It.IsAny<Guid>()), Times.Once);
        }
    }
}
