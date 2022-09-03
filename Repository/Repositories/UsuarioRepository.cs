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
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly DbSet<Usuario> _usuarios;

        public UsuarioRepository(AppDbContext easyChefContext) : base(easyChefContext)
        {
            _usuarios = easyChefContext.Usuarios;
        }

        public Usuario? GetUsuarioByEmail(string email)
        {
            return _usuarios.Where(usuario => usuario.Email.Equals(email))
                .FirstOrDefault();
        }

        public Usuario? GetUsuarioByEmailSenha(string email, string senha)
        {
            return _usuarios.Where(usuario => usuario.Email.Equals(email) && usuario.Senha.Equals(senha))
                .FirstOrDefault();
        }
    }
}
