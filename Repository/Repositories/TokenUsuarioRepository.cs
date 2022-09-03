using Domain.Entities;
using Data.Contexts;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

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
