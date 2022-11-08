using Data.Contexts;
using Domain.Entities;
using Domain.Models.Receita;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class ReceitaRepository : RepositoryBase<Receita>, IReceitaRepository
    {
        private readonly DbSet<Receita> _receitas;

        public ReceitaRepository(AppDbContext easyChefContext) : base(easyChefContext)
        {
            _receitas = easyChefContext.Receitas;
        }

        public ReceitaModel? GetReceita(Guid id, Guid empresaId)
        {
            return _receitas.Where(receita => receita.Id == id && receita.EmpresaId == empresaId && receita.Habilitado)
            .Include(receita => receita.Empresa)
            .Select(receita => new ReceitaModel
            {
                Id = receita.Id,
                Nome = receita.Nome,
                Imagem = receita.Imagem,
            }
            )
            .FirstOrDefault();
        }

        public List<ReceitaModel> GetReceitas(Guid empresaId, string busca, int pagina, int quantidade)
        {
            return _receitas
               .Where(receita => receita.Nome.ToLower().Contains(busca.ToLower()) && receita.EmpresaId == empresaId && receita.Habilitado)
               .Include(receita => receita.Empresa)
               .OrderBy(receita => receita.Id)
               .Skip((pagina - 1) * quantidade) 
               .Take(quantidade)
               .Select(receita => new ReceitaModel
               {
                   Id = receita.Id,
                   Imagem = receita.Imagem,
                   Descricao = receita.Descricao,
                   Valor = receita.Valor,
                   Nome = receita.Nome,
                   Ingredientes = receita.ReceitasIngredientes.Select(ingrediente => new ReceitaIngredienteModel
                   {
                       Id = ingrediente.Id,
                       IngredienteId = ingrediente.IngredienteId,
                       Nome = ingrediente.Ingrediente.Nome,
                       Imagem = ingrediente.Ingrediente.Imagem,
                       Quantidade = ingrediente.Quantidade,
                       Valor = ingrediente.Ingrediente.Valor,
                       UnidadeMedidaId = ingrediente.UnidadeMedidaId,
                       UnidadeMedida = ingrediente.UnidadeMedida.Nome
                   }).ToList()
               }
               )
               .ToList();
        }

        public int GetQuantidadeReceitas(Guid empresaId, string busca)
        {
            return _receitas
               .Where(receita => receita.Nome.ToLower().Contains(busca.ToLower()) && receita.EmpresaId == empresaId && receita.Habilitado)
               .Count();
        }
    }
}