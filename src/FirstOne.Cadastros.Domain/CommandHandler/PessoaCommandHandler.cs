using FirstOne.Cadastros.Domain.Command;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.CommandHandler
{
    public class PessoaCommandHandler :
        IRequestHandler<AdicionarPessoaCommand, bool>
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaCommandHandler(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public Task<bool> Handle(AdicionarPessoaCommand request, CancellationToken cancellationToken)
        {
            var pessoa = new Pessoa(Guid.NewGuid(), request.Nome);
            _pessoaRepository.Adicionar(pessoa);

            return Task.FromResult(true);
        }
    }
}
