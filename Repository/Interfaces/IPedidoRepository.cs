using Domain.Entities;
using Domain.Models.Pedido;

namespace Repository.Interfaces
{
    public interface IPedidoRepository : IRepositoryBase<Pedido>
    {
        public List<PedidoModel> GetPedidos(Guid empresaId, string busca = "", int pagina = 1, int quantidade = 15);
        public int GetQuantidadePedidos(Guid empresaId, string busca = "");
    }
}
