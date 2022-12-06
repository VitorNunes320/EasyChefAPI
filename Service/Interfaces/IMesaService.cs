using Domain.Models;
using Domain.Models.Pedido;

namespace Service.Interfaces
{
    public interface IMesaService
    {
        MesaModel? GetMesa(Guid id, Guid empresaId);
        Paginacao<List<MesaModel>> GetMesas(Guid empresaId, string busca, int pagina, int quantidade);
        public void CreateMesa(MesaModel model, Guid empresaId, string usuario);
    }
}
