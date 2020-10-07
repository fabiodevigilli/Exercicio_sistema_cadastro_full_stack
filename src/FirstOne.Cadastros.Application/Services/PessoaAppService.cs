﻿using AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Interfaces;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Application.Services
{
    public class PessoaAppService : IPessoaAppService
    {
        private readonly IMapper _mapper;
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaAppService(IMapper mapper,
                                IPessoaRepository pessoaRepository)
        {
            _mapper = mapper;
            _pessoaRepository = pessoaRepository;
        }

        public ValidationResult Adicionar(PessoaViewModel pessoaViewModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PessoaViewModel> ObterTodos()
        {
            return _mapper.Map<IEnumerable<PessoaViewModel>>(_pessoaRepository.ObterTodos());
        }       
    }
}
