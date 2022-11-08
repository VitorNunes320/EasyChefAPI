using Data.Contexts;
using Domain.Entities;
using Domain.Models.Receita;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class UnidadeMedidaRepository : RepositoryBase<UnidadeMedida>, IUnidadeMedidaRepository
    {
        private readonly DbSet<UnidadeMedida> _unidadesMedidas;

        public UnidadeMedidaRepository(AppDbContext easyChefContext) : base(easyChefContext)
        {
            _unidadesMedidas = easyChefContext.UnidadesMedidas;
        }

        public UnidadeMedidaModel? GetUnidadeMedida(Guid id)
        {
            return _unidadesMedidas.Where(unidadeMedida => unidadeMedida.Id == id && unidadeMedida.Habilitado)
            .Select(unidadeMedida => new UnidadeMedidaModel
            {
                Id = unidadeMedida.Id,
                Nome = unidadeMedida.Nome,
            }
            )
            .FirstOrDefault();
        }

        public List<UnidadeMedidaModel> GetUnidadesMedidas(string busca, int pagina, int quantidade)
        {
            return _unidadesMedidas.Where(unidadeMedida => unidadeMedida.Nome.ToLower().Contains(busca.ToLower()) && unidadeMedida.Habilitado)
               .OrderBy(unidadeMedida => unidadeMedida.CriadoEm)
               .Skip((pagina - 1) * quantidade)
               .Take(quantidade)
               .Select(unidadeMedida => new UnidadeMedidaModel
               {
                   Id = unidadeMedida.Id,
                   Nome = unidadeMedida.Nome,
               }
               )
               .ToList();
        }

        public int GetQuantidadeUnidadeMedidas(string busca)
        {
            return _unidadesMedidas.Where(unidadeMedida => unidadeMedida.Nome.ToLower().Contains(busca.ToLower()) && unidadeMedida.Habilitado)
                .Count();
        }
    }
}