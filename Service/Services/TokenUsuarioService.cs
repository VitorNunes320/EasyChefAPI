using Domain.Entities;
using Domain.Exceptions;
using Domain.Helpers;
using Domain.Models.Autenticacao;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;
using Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Services
{
    public class TokenUsuarioService : ITokenUsuarioService
    {

        private readonly AppSettings _appSettings;
        private readonly ITokenUsuarioRepository _tokenUsuarioRepository;
        private readonly IPerfilUsuarioRepository _perfilUsuarioRepository;

        public TokenUsuarioService(IOptions<AppSettings> appSettings, ITokenUsuarioRepository tokenUsuarioRepository,
            IPerfilUsuarioRepository perfilUsuarioRepository)
        {
            _tokenUsuarioRepository = tokenUsuarioRepository;
            _appSettings = appSettings.Value;
            _perfilUsuarioRepository = perfilUsuarioRepository;
        }

        public TokenResponse GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JWTSecret);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Email.ToString()),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
            };

            var papeis = _perfilUsuarioRepository.GetPerfisUsuarios(usuario.Id);
            papeis.ForEach(papel =>
            {
                claims.Add(new Claim(ClaimTypes.Role, papel.Perfil.TipoPerfil.ToString()));
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            TokenUsuario refreshToken = GerarTokenUsuario(usuario.Id, 3);

            return new TokenResponse { RefreshToken = refreshToken.Token, Token = jwtToken };
        }

        public TokenUsuario GerarTokenUsuario(Guid usuarioId, int dias, int? tamanho = null)
        {
            string token;
            do
            {
                token = tamanho != null ? RandomString((int)tamanho) : (RandomString(35) + Guid.NewGuid());
            } while (_tokenUsuarioRepository.GetByToken(token) != null);

            var refreshToken = new TokenUsuario()
            {
                UsuarioId = usuarioId,
                CriadoEm = DateTime.UtcNow,
                ExpiraEm = DateTime.UtcNow.AddDays(dias),
                Habilitado = true,
                Token = token
            };

            _tokenUsuarioRepository.Add(refreshToken);
            return refreshToken;
        }

        private static string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public TokenUsuario? ValidarTokenUsuario(string token)
        {
            ResponseBase<object> response = new ResponseBase<object>();

            var tokenExiste = _tokenUsuarioRepository.GetByToken(token);

            if (tokenExiste == null) throw new TokenNaoExisteException();

            if (!tokenExiste.Habilitado) throw new TokenUtilizadoException();

            if (tokenExiste.ExpiraEm < DateTime.UtcNow) throw new TokenExpirouException();

            tokenExiste.Habilitado = false;
            _tokenUsuarioRepository.Edit(tokenExiste);

            return tokenExiste;
        }
    }

}
