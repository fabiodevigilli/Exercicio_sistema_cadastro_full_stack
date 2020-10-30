using FirstOne.Cadastros.Domain.Commands.UsuarioCommands;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.CommandHandler
{
    public class UsuarioCommandHandler :
        IRequestHandler<AdicionarUsuarioCommand, Unit>
    //IRequestHandler<AtualizarPessoaCommand, Unit>,
    //IRequestHandler<RemoverPessoaCommand, Unit>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        public async Task<Unit> Handle(AdicionarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = new Usuario(Guid.NewGuid(),request.Email, request.Senha, request.PessoaId, request.Role);
            _usuarioRepository.Adicionar(usuario);
            await _usuarioRepository.UnitOfWork.Commit();

            return Unit.Value;
        }
    }
}
