using Domain.Entities;
using Domain.Models;

namespace Repository.Interfaces
{
    public interface IPerfilRepository : IRepositoryBase<Perfil>
    {
        public List<PerfilResponse> GetPerfis();
    }
}
