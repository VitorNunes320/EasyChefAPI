using Data.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class TokenUsuarioRepository : RepositoryBase<TokenUsuario>, ITokenUsuarioRepository
    {
        private readonly DbSet<TokenUsuario> _tokensUsuarios;

        public TokenUsuarioRepository(AppDbContext easyChefContext) : base(easyChefContext)
        {
            _tokensUsuarios = easyChefContext.TokensUsuarios;
        }

        public TokenUsuario? GetByToken(string token)
        {
            return _tokensUsuarios.Where(tokenUsuario => tokenUsuario.Token.Equals(token) && tokenUsuario.Habilitado)
                .FirstOrDefault();
        }
    }
}
