using Data.Contexts;
using Domain.Entities;
using Domain.Models.Pedido;
using Domain.Models.Receita;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class MesaRepository : RepositoryBase<Mesa>, IMesaRepository
    {
        private readonly DbSet<Mesa> _mesas;

        public MesaRepository(AppDbContext easyChefContext) : base(easyChefContext)
        {
            _mesas = easyChefContext.Mesas;
        }

        public MesaModel? GetMesa(Guid id, Guid empresaId)
        {
            return _mesas.Where(mesa => mesa.Id == id && mesa.EmpresaId == empresaId && mesa.Habilitado)
            .Select(mesa => new MesaModel
            {
                Id = mesa.Id,
                Nome = mesa.Nome,
                Codigo = mesa.Codigo.ToString(),
                Ocupada = mesa.Ocupada,
            }
            )
            .FirstOrDefault();
        }

        public List<MesaModel> GetMesas(Guid empresaId, string busca, int pagina, int quantidade)
        {
            return _mesas
               .Where(mesa => mesa.Nome.ToLower().Contains(busca.ToLower()) && mesa.EmpresaId == empresaId && mesa.Habilitado)
               .OrderBy(mesa => mesa.CriadoEm)
               .Skip((pagina - 1) * quantidade)
               .Take(quantidade)
               .Select(mesa => new MesaModel
               {
                   Id = mesa.Id,
                   Nome = mesa.Nome,
                   Codigo = mesa.Codigo.ToString(),
                   Ocupada = mesa.Ocupada,
               }
               )
               .ToList();
        }

        public int GetQuantidadeMesas(Guid empresaId, string busca)
        {
            return _mesas
               .Where(mesa => mesa.Nome.ToLower().Contains(busca.ToLower()) && mesa.EmpresaId == empresaId && mesa.Habilitado)
               .Count();
        }
    }
}