using Domain.Entities;
using Domain.Models.Receita;

namespace Repository.Interfaces
{
    public interface IUnidadeMedidaRepository : IRepositoryBase<UnidadeMedida>
    {
        public List<UnidadeMedidaModel> GetUnidadesMedidas(string busca, int pagina = 1, int quantidade = 15);

        public int GetQuantidadeUnidadeMedidas(string busca);

        public UnidadeMedidaModel? GetUnidadeMedida(Guid id);
    }
}