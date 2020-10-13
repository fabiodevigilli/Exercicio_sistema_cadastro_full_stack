using AutoMapper;
using FirstOne.Cadastros.Application.AutoMapper;
using FirstOne.Cadastros.Application.Services;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Command;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Domain.Messaging;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FirstOne.Cadastros.Application.Tests
{
    public class PessoaAppServiceTests
    {
        private readonly AutoMocker _autoMocker;
        private readonly PessoaAppService _pessoaAppService;

        public PessoaAppServiceTests()
        {
            _autoMocker = new AutoMocker();

            var mapper = new MapperConfiguration(cfg => 
            { 
                cfg.AddProfile(new DomainToViewModelMappingProfile()); 
            }).CreateMapper();
            var pessoaRepository = _autoMocker.GetMock<IPessoaRepository>();
            var mediatorHandler = _autoMocker.GetMock<IMediatorHandler>();

            _pessoaAppService = new PessoaAppService(mapper, pessoaRepository.Object, mediatorHandler.Object);
        }



        [Fact(DisplayName = "ObterTodos deve obter Pessoas")]
        [Trait("Categoria", "PessoaAppService")]
        public void ObterTodos_DeveObterPessoas()
        {
            // Arrange
            var pessoas = new List<Pessoa>()
            {
                new Pessoa(Guid.NewGuid(), "Pessoa 1"),
                new Pessoa(Guid.NewGuid(), "Pessoa 2")
            };
            _autoMocker.GetMock<IPessoaRepository>().Setup(e => e.ObterTodos()).Returns(pessoas);
            
            // Act
            var result = _pessoaAppService.ObterTodos();

            // Assert
            _autoMocker.GetMock<IPessoaRepository>().Verify(e => e.ObterTodos(), Times.Once);
            Assert.Equal(2, result.Count());
        }

        [Fact(DisplayName = "Deve_Adicionar_Pessoa")]
        [Trait("Categoria", "PessoaAppService")]
        public async Task Deve_Adicionar_Pessoa()
        {
            // Arrange
            var pessoaViewModel = new PessoaViewModel()
            {
                Nome = "Pessoa 1"
            };

            // Act
            var result = await _pessoaAppService.Adicionar(pessoaViewModel);

            // Assert
            Assert.True(result.IsValid);
            _autoMocker.GetMock<IMediatorHandler>().Verify(e => e.EnviarCommand(It.IsAny<AdicionarPessoaCommand>()), Times.Once);
        }

        [Fact(DisplayName = "Nao_Deve_Adicionar_Pessoa_Validator_Nome")]
        [Trait("Categoria", "PessoaAppService")]
        public async Task Nao_Deve_Adicionar_Pessoa_Validator_Nome()
        {
            // Arrange
            var pessoaViewModel = new PessoaViewModel()
            {
                Nome = ""
            };

            // Act
            var result = await _pessoaAppService.Adicionar(pessoaViewModel);

            // Assert
            _autoMocker.GetMock<IMediatorHandler>().Verify(e => 
            e.PublicarDomainNotification(It.Is<DomainNotification>(dn => 
            dn.Value == "Por favor, informe o Nome da Pessoa")), Times.Once);
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Equal("Por favor, informe o Nome da Pessoa", result.Errors.First().ErrorMessage);
        }
    }
}
