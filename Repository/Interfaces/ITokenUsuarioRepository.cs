using Domain.Entities;

namespace Repository.Interfaces
{
    public interface ITokenUsuarioRepository : IRepositoryBase<TokenUsuario>
    {
        public TokenUsuario? GetByToken(string token);
    }
}
