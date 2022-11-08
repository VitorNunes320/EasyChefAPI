using Domain.Entities;
using Domain.Models;
using Domain.Models.Receita;

namespace Service.Interfaces
{
    public interface IIngredienteService
    {
        public bool CreateIngrediente(IngredienteModel model, string usuarioCriou);

        public bool DeleteIngrediente(Guid id, string usuarioAtualizou, Guid empresaId);

        public IngredienteModel? GetIngrediente(Guid id, Guid empresaId);

        public Paginacao<List<IngredienteModel>> GetIngredientes(Guid empresaId, string busca, int pagina, int quantidade);

        public bool UpdateIngrediente(IngredienteModel model, string usuarioAtualizou, Guid empresaId);
    }
}
