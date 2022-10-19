using Domain.Entities;
using Domain.Models.Autenticacao;

namespace Service.Interfaces
{
    public interface ITokenUsuarioService
    {
        public TokenResponse GerarToken(Usuario usuario);

        public TokenUsuario GerarTokenUsuario(Guid usuarioId, int dias, int? tamanho);

        public TokenUsuario? ValidarTokenUsuario(string token);
    }
}
