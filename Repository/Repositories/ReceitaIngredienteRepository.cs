using Data.Contexts;
using Domain.Entities;
using Domain.Models.Receita;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class ReceitaIngredienteRepository : RepositoryBase<ReceitaIngrediente>, IReceitaIngredienteRepository
    {
        private readonly DbSet<ReceitaIngrediente> _receitasIngredientes;

        public ReceitaIngredienteRepository(AppDbContext easyChefContext) : base(easyChefContext)
        {
            _receitasIngredientes = easyChefContext.ReceitasIngredientes;
        }
    }
}