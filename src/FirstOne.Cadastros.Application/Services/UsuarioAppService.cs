using AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Commands.UsuarioCommands;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Services
{
    public class UsuarioAppService : AppService, IUsuarioAppService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioAppService(IMapper mapper,
                                IUsuarioRepository usuarioRepository,
                                IMediatorHandler mediatorHandler) : base(mediatorHandler)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        public async Task Adicionar(UsuarioViewModel usuarioViewModel)
        {
            var command = new AdicionarUsuarioCommand(usuarioViewModel.Email, usuarioViewModel.Senha, usuarioViewModel.PessoaId);
            if (!command.IsValid())
            {
                await PublicarErrosDeValidacao(command);
                return;
            }

            await _mediatorHandler.EnviarCommand(command);
        }
    }
}
