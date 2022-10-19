using Domain.Entities;

namespace Repository.Interfaces
{
    public interface IPerfilUsuarioRepository : IRepositoryBase<PerfilUsuario>
    {
        public List<PerfilUsuario> GetPerfisUsuarios(Guid usuarioId);
    }
}
