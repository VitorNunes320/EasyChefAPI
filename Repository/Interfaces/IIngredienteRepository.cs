using Domain.Entities;
using Domain.Models;
using Domain.Models.Receita;

namespace Repository.Interfaces
{
    public interface IIngredienteRepository : IRepositoryBase<Ingrediente>
    {
        public IngredienteModel? GetIngrediente(Guid id, Guid empresaId);

        public List<IngredienteModel> GetIngredientes(Guid empresaId, string busca = "", int pagina = 1, int quantidade = 15);

        public int GetQuantidadeIngredientes(Guid empresaId, string busca);
    }
}