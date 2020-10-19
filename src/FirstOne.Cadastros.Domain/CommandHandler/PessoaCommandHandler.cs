using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.CommandHandler
{
    public class PessoaCommandHandler :
        IRequestHandler<AdicionarPessoaCommand, Unit>,
        IRequestHandler<AtualizarPessoaCommand, Unit>,
        IRequestHandler<RemoverPessoaCommand, Unit>
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaCommandHandler(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task<Unit> Handle(AdicionarPessoaCommand request, CancellationToken cancellationToken)
        {
            var pessoa = new Pessoa(Guid.NewGuid(), request.Nome);
            _pessoaRepository.Adicionar(pessoa);

            return Unit.Value;
        }

        public async Task<Unit> Handle(AtualizarPessoaCommand request, CancellationToken cancellationToken)
        {
            var pessoa = new Pessoa(request.Id, request.Nome);
            _pessoaRepository.Atualizar(pessoa);

            return Unit.Value;
        }

        public async Task<Unit> Handle(RemoverPessoaCommand request, CancellationToken cancellationToken)
        {
            _pessoaRepository.Remover(request.Id);

            return Unit.Value;
        }
    }
}
