using AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Command;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Application.Services
{
    public class PessoaAppService : IPessoaAppService
    {
        private readonly IMapper _mapper;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PessoaAppService(IMapper mapper,
                                IPessoaRepository pessoaRepository,
                                IMediatorHandler mediatorHandler)
        {
            _mapper = mapper;
            _pessoaRepository = pessoaRepository;
            _mediatorHandler = mediatorHandler;
        }

        public ValidationResult Adicionar(PessoaViewModel pessoaViewModel)
        {
            var command = new AdicionarPessoaCommand(pessoaViewModel.Nome);
            if (!command.IsValid())
            {
                return command.ValidationResult;
            }

            var pessoa = new Pessoa(Guid.NewGuid(), command.Nome);
            _pessoaRepository.Adicionar(pessoa);
            _mediatorHandler.EnviarCommand(command);
            return command.ValidationResult;
        }

        public IEnumerable<PessoaViewModel> ObterTodos()
        {
            return _mapper.Map<IEnumerable<PessoaViewModel>>(_pessoaRepository.ObterTodos());
        }       
    }
}
