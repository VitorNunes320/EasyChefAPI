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
        private readonly IReceitaRepository _receitaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public PedidoService(
            IPedidoRepository pedidoRespository,
            IUsuarioRepository usuarioRepository,
            IReceitaRepository receitaRepository)
        {
            _pedidoRespository = pedidoRespository;
            _usuarioRepository = usuarioRepository;
            _receitaRepository = receitaRepository;
        }

        public Paginacao<List<PedidoModel>> GetPedidos(Guid empresaId, string busca = "", int pagina = 1, int quantidade = 15)
        {
            return new Paginacao<List<PedidoModel>>
            {
                Dados = _pedidoRespository.GetPedidos(empresaId, busca, pagina, quantidade),
                Quantidade = _pedidoRespository.GetQuantidadePedidos(empresaId, busca)
            };
        }

        public bool CreatePedido(PedidoModel model)
        {
            var vendedor = _usuarioRepository.GetById(model.VendedorId);
            var pedido = new Pedido
            {
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
                var receita = _receitaRepository.GetById(produto.Id);
                if (receita == null)
                {
                    return false;
                }

                var produtoPedido = new ProdutoPedido
                {
                    ReceitaId = produto.Id,
                    Quantidade = produto.Quantidade,
                    NomeCliente = produto.NomeCliente,
                    Valor = receita.Valor
                };
                pedido.ProdutosPedidos.Add(produtoPedido);
                pedido.ValorTotal += (receita.Valor * produto.Quantidade);
            }

            pedido.QuantidadeItens = pedido.ProdutosPedidos.Count;
            _pedidoRespository.Add(pedido);
            return true;
        }
    }
}
