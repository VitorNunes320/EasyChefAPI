using Data.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

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
