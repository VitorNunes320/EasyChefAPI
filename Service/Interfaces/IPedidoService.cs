using Domain.Models;
using Domain.Models.Pedido;

namespace Service.Interfaces
{
    public interface IPedidoService
    {
        public List<PedidoModel> GetPedidos(string busca = "", int pagina = 1, int quantidade = 15);

        public bool CreatePedido(PedidoModel model);
    }
}
