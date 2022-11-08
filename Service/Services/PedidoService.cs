using CrossCutting.Security;
using CrossCutting.Utils;
using Domain.Entities;
using Domain.Enums;
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
        private readonly IUsuarioRepository _usuarioRepository;

        public PedidoService(IPedidoRepository pedidoRespository, IUsuarioRepository usuarioRepository)
        {
            _pedidoRespository = pedidoRespository;
            _usuarioRepository = usuarioRepository;
        }

        public List<PedidoModel> GetPedidos(string busca = "", int pagina = 1, int quantidade = 15)
        {
            return null;
        }

        public bool CreatePedido(PedidoModel model)
        {
            var vendedor = _usuarioRepository.GetById(model.VendedorId);
            var pedido = new Pedido
            {
                ValorTotal = 0,
                Observacoes = model.Observacoes,
                Status = StatusPedido.NaoIniciado,
                VendedorId = model.VendedorId,
                EmpresaId = vendedor.EmpresaId,
                TipoPedido = model.TipoPedido,
                MesaId = model.MesaId,
                ProdutosPedidos = new List<ProdutoPedido>()
            };

            foreach (var produto in model.Produtos)
            {
                var produtoPedido = new ProdutoPedido
                {
                    ReceitaId = produto.Id,
                    Quantidade = produto.Quantidade,
                    NomeCliente = produto.NomeCliente,
                };

            }

            return true;
        }
    }
}
