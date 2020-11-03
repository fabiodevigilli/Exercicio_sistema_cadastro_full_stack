using AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Services
{
    public class PessoaAppService : AppService, IPessoaAppService
    {
        private readonly IMapper _mapper;
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaAppService(IMapper mapper,
                                IPessoaRepository pessoaRepository,
                                IMediatorHandler mediatorHandler) : base(mediatorHandler)
        {
            _mapper = mapper;
            _pessoaRepository = pessoaRepository;
        }

        public async Task Adicionar(PessoaViewModel pessoaViewModel)
        {
            var command = new AdicionarPessoaCommand(pessoaViewModel.Nome);
            if (!command.IsValid())
            {
                await PublicarErrosDeValidacao(command);
                return;
            }

            await _mediatorHandler.EnviarCommand(command);
        }

        public async Task Atualizar(PessoaViewModel pessoaViewModel)
        {
            var command = new AtualizarPessoaCommand(pessoaViewModel.Id, pessoaViewModel.Nome);
            if (!command.IsValid())
            {
                await PublicarErrosDeValidacao(command);
                return;
            }

            await _mediatorHandler.EnviarCommand(command);
        }

        public async Task Remover(Guid id)
        {
            var command = new RemoverPessoaCommand(id);
            if (!command.IsValid())
            {
                await PublicarErrosDeValidacao(command);
                return;
            }

            await _mediatorHandler.EnviarCommand(command);
        }

        public IEnumerable<PessoaViewModel> ObterTodos()
        {
            return _mapper.Map<IEnumerable<PessoaViewModel>>(_pessoaRepository.ObterTodos());
        }

        public PessoaViewModel ObterPorId(Guid id)
        {
            return _mapper.Map<PessoaViewModel>(_pessoaRepository.ObterPorId(id));
        }
    }
}
