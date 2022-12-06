using Domain.Models;
using Domain.Models.Pedido;

namespace Service.Interfaces
{
    public interface IPedidoService
    {
        public Paginacao<List<PedidoModel>> GetPedidos(Guid empresaId, string busca = "", int pagina = 1, int quantidade = 15);

        public bool CreatePedido(PedidoModel model);
    }
}
