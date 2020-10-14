using AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Command;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Domain.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task Adicionar(PessoaViewModel pessoaViewModel)
        {
            var command = new AdicionarPessoaCommand(pessoaViewModel.Nome);
            if (!command.IsValid())
            {
                foreach (var error in command.ValidationResult.Errors)
                {
                    await _mediatorHandler.PublicarDomainNotification(new DomainNotification(error.ErrorMessage));
                }
                return;
            }

            await _mediatorHandler.EnviarCommand(command);
        }

        public Task Atualizar(PessoaViewModel pessoaViewModel)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PessoaViewModel> ObterTodos()
        {
            return _mapper.Map<IEnumerable<PessoaViewModel>>(_pessoaRepository.ObterTodos());
        }
    }
}
