using Domain.Entities;
using Domain.Models;
using Domain.Models.Pedido;
using Domain.Models.Receita;

namespace Repository.Interfaces
{
    public interface IMesaRepository : IRepositoryBase<Mesa>
    {
        MesaModel? GetMesa(Guid id, Guid empresaId);
        List<MesaModel> GetMesas(Guid empresaId, string busca, int pagina, int quantidade);
        int GetQuantidadeMesas(Guid empresaId, string busca);
    }
}