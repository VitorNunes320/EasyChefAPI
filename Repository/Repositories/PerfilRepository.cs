using Data.Contexts;
using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class PerfilRepository : RepositoryBase<Perfil>, IPerfilRepository
    {
        private readonly DbSet<Perfil> _perfis;

        public PerfilRepository(AppDbContext easyChefContext) : base(easyChefContext)
        {
            _perfis = easyChefContext.Perfis;
        }

        public List<PerfilResponse> GetPerfis()
        {
            return _perfis.Select(perfil => new PerfilResponse(perfil.Id, perfil.Descricao)).ToList();
        }
    }
}
