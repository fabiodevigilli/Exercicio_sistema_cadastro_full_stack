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
            var command = new AdicionarUsuarioCommand(usuarioViewModel.Email, usuarioViewModel.Senha, usuarioViewModel.PessoaId, usuarioViewModel.Role);
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
                new Claim(ClaimTypes.Email, usuario.Email),
               // new Claim(ClaimTypes.Role, usuario.Role)
               new Claim("Role", usuario.Role)
            };
           
            claims.AddRange(usuario.UsuarioClaims.Select(claim => new Claim(Convert.ToString(claim.Entidade), claim.Endpoint)));

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

        public async Task AtualizarClaims(UsuarioClaimViewmodel usuarioClaimViewmodel)
        {
            var usuario = _usuarioRepository.Search(x => x.Id == usuarioClaimViewmodel.UsuarioId).FirstOrDefault();
            
            foreach (var claim in usuario.UsuarioClaims)
            {
                _usuarioRepository.RemoverClaim(claim);
            }

            foreach (var claim in usuarioClaimViewmodel.UsuarioClaims)
            {
                _usuarioRepository.AdicionarClaim(new UsuarioClaim(Guid.NewGuid(), usuarioClaimViewmodel.UsuarioId, claim.Entidade, claim.Endpoint));    
            }

            await _usuarioRepository.UnitOfWork.Commit();
        }

        //public UsuarioPermissoesViewmodel ObterPermissoes(Guid usuarioid)
        //{
        //    var permissoesUsuario = _usuarioRepository.ObterPermissoes(usuarioid);
        //    var usuarioPermissoesViewmodel = new List<PermissoesViewModel>();
        //    foreach (var permissoes in permissoesUsuario)
        //    {
        //        usuarioPermissoesViewmodel.Add(new PermissoesViewModel() 
        //        { 
        //            Rotinas = permissoes.RotinaEntidades, 
        //            Values = permissoes.Permissao.Split(",") 
        //        });
        //    }

        //    return new UsuarioPermissoesViewmodel { UsuarioId = usuarioid, Permissoes = usuarioPermissoesViewmodel };
        //}
    }
}
