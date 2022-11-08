using Data.Contexts;
using Domain.Entities;
using Domain.Models.Receita;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class IngredienteRepository : RepositoryBase<Ingrediente>, IIngredienteRepository
    {
        private readonly DbSet<Ingrediente> _ingredientes;

        public IngredienteRepository(AppDbContext easyChefContext) : base(easyChefContext)
        {
            _ingredientes = easyChefContext.Ingredientes;
        }

        public IngredienteModel? GetIngrediente(Guid id, Guid empresaId)
        {
            return _ingredientes.Where(ingrediente => ingrediente.Id == id && ingrediente.EmpresaId == empresaId && ingrediente.Habilitado)
            .Include(ingrediente => ingrediente.UnidadeMedida)
            .Select(ingrediente => new IngredienteModel
            {
                Id = ingrediente.Id,
                Nome = ingrediente.Nome,
                Imagem = ingrediente.Imagem,
                UnidadeMedidaId = ingrediente.UnidadeMedidaId,
                UnidadeMedida = ingrediente.UnidadeMedida.Nome,
                Descricao = ingrediente.Descricao,
                Valor = ingrediente.Valor,
            }
            )
            .FirstOrDefault();
        }

        public List<IngredienteModel> GetIngredientes(Guid empresaId, string busca, int pagina, int quantidade)
        {
            return _ingredientes
               .Where(ingrediente => ingrediente.Nome.ToLower().Contains(busca.ToLower()) && ingrediente.EmpresaId == empresaId && ingrediente.Habilitado)
               .Include(ingrediente => ingrediente.UnidadeMedida)
               .OrderBy(ingrediente => ingrediente.CriadoEm)
               .Skip((pagina - 1) * quantidade)
               .Take(quantidade)
               .Select(ingrediente => new IngredienteModel
               {
                   Id = ingrediente.Id,
                   Imagem = ingrediente.Imagem,
                   Nome = ingrediente.Nome,
                   UnidadeMedida = ingrediente.UnidadeMedida.Nome,
                   Descricao = ingrediente.Descricao,
                   Valor = ingrediente.Valor,
               }
               )
               .ToList();
        }

        public int GetQuantidadeIngredientes(Guid empresaId, string busca)
        {
            return _ingredientes
               .Where(ingrediente => ingrediente.Nome.ToLower().Contains(busca.ToLower()) && ingrediente.EmpresaId == empresaId && ingrediente.Habilitado)
               .Count();
        }
    }
}