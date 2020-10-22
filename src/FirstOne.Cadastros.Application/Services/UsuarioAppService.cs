using AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.Token;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Commands.UsuarioCommands;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Services
{
    public class UsuarioAppService : AppService, IUsuarioAppService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly TokenSettings _tokenSettings;

        public UsuarioAppService(IMapper mapper,
                                IUsuarioRepository usuarioRepository,
                                IMediatorHandler mediatorHandler,
                                IOptions<TokenSettings> tokenSettings) : base(mediatorHandler)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _tokenSettings = tokenSettings.Value;
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
        
        public IEnumerable<UsuarioViewModel> ObterTodos()
        {
            return _mapper.Map<IEnumerable<UsuarioViewModel>>(_usuarioRepository.ObterTodos());
        }

        public string Login(string email, string password)
        {
            var usuario = _usuarioRepository.Search(x => x.Email == email && x.Senha == password).FirstOrDefault();

            if (usuario == null)
            {
                return null;
            }

            return GenerateToken(usuario);
        }

        private string GenerateToken(Usuario usuario)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var claimdIdentity = new ClaimsIdentity();
            claimdIdentity.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();           

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenSettings.Issuer,
                Audience = _tokenSettings.Audience,
                Expires = DateTime.Now.AddHours(_tokenSettings.Expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenSettings.Secret)), SecurityAlgorithms.HmacSha256Signature),
                Subject = claimdIdentity
            });

            return tokenHandler.WriteToken(token);
        }
    }
}
