using CrossCutting.Security;
using CrossCutting.Utils;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.Pedido;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRespository;

        public PedidoService(IPedidoRepository pedidoRespository)
        {
            _pedidoRespository = pedidoRespository;
        }

        public List<PedidoModel> GetPedidos(string busca = "", int pagina = 1, int quantidade = 15)
        {
            return null;
        }

        public bool CreatePedido(PedidoModel model)
        {
            var pedido = new Pedido
            {
                ProdutosPedidos = new List<ProdutoPedido>()
            };

            foreach (var produto in model.Produtos)
            {

            }

            return true;
        }
    }
}
