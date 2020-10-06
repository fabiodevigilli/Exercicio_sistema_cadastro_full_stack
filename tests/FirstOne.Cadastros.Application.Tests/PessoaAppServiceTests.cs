using AutoMapper;
using FirstOne.Cadastros.Application.AutoMapper;
using FirstOne.Cadastros.Application.Services;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
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

            _pessoaAppService = new PessoaAppService(mapper, pessoaRepository.Object);
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
    }
}
