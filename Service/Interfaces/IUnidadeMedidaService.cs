using Domain.Entities;
using Domain.Models;
using Domain.Models.Receita;

namespace Service.Interfaces
{
    public interface IUnidadeMedidaService
    {
        public void CreateUnidadeMedida(UnidadeMedidaModel model, string usuarioCriou);

        public UnidadeMedidaModel? GetUnidadeMedida(Guid id);

        public Paginacao<List<UnidadeMedidaModel>> GetUnidadesMedidas(string busca, int pagina, int quantidade);

        public void RemoveUnidadeMedida(Guid id, string usuarioAtualizou);

        public void UpdateUnidadeMedida(UnidadeMedida model, string usuarioAtualizou);
    }
}
