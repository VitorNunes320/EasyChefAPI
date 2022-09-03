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
    public class PerfilUsuarioRepository : RepositoryBase<PerfilUsuario>, IPerfilUsuarioRepository
    {
        private readonly DbSet<PerfilUsuario> _perfisUsuarios;

        public PerfilUsuarioRepository(AppDbContext easyChefContext) : base(easyChefContext)
        {
            _perfisUsuarios = easyChefContext.PerfisUsuarios;
        }

        public List<PerfilUsuario> GetPerfisUsuarios(Guid usuarioId)
        {
            return _perfisUsuarios.Where(perfil => perfil.UsuarioId.Equals(usuarioId))
                .Include(perfil => perfil.Perfil)
                .ToList();
        }
    }
}
