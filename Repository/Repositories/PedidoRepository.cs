using Data.Contexts;
using Domain.Entities;
using Domain.Models.Pedido;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        private readonly DbSet<Pedido> _pedidos;

        public PedidoRepository(AppDbContext easyChefContext) : base(easyChefContext)
        {
            _pedidos = easyChefContext.Pedidos;
        }

        public List<PedidoModel> GetPedidos(Guid empresaId, string busca, int pagina, int quantidade)
        {
            return _pedidos.Where(pedido =>
                pedido.EmpresaId == empresaId && (
                    string.IsNullOrWhiteSpace(busca) ? true :
                    (
                        pedido.Codigo.ToString().Contains(busca) ||
                        pedido.QuantidadeItens.ToString().Contains(busca) ||
                        pedido.Mesa.Codigo.ToString().Contains(busca) ||
                        pedido.Mesa.Nome.ToLower().Contains(busca.ToLower()) ||
                        pedido.ProdutosPedidos.Any(produto =>
                            produto.NomeCliente.ToLower().Contains(busca.ToLower()) ||
                            produto.Receita.Nome.ToLower().Contains(busca.ToLower())
                        )
                    )
                )
            )
            .OrderBy(pedido => pedido.CriadoEm)
            .Skip((pagina - 1) * quantidade)
            .Take(quantidade)
            .Select(pedido => new PedidoModel
            {
                Id = pedido.Id,
                Codigo = pedido.Codigo.ToString(),
                Data = pedido.CriadoEm,
                ValorTotal = pedido.ValorTotal,
                QuantidadeItens = pedido.QuantidadeItens,
                Observacoes = pedido.Observacoes,
                VendedorNome = pedido.Vendedor.Nome,
                VendedorId = pedido.VendedorId,
                Status = pedido.Status,
                TipoPedido = pedido.TipoPedido,
                MesaId = pedido.MesaId,
                Produtos = pedido.ProdutosPedidos.Select(produtoPedido => new ProdutoPedidoModel
                {
                    Id = produtoPedido.Id,
                    Imagem = produtoPedido.Receita.Imagem,
                    Nome = produtoPedido.Receita.Nome,
                    NomeCliente = produtoPedido.NomeCliente,
                    Valor = produtoPedido.Valor,
                    Quantidade = produtoPedido.Quantidade
                })
                .ToList()
            })
            .ToList();
        }

        public int GetQuantidadePedidos(Guid empresaId, string busca)
        {
            return _pedidos.Where(pedido =>
                pedido.EmpresaId == empresaId && (
                    string.IsNullOrWhiteSpace(busca) ? true :
                    (
                        pedido.Codigo.ToString().Contains(busca) ||
                        pedido.QuantidadeItens.ToString().Contains(busca) ||
                        pedido.Mesa.Codigo.ToString().Contains(busca) ||
                        pedido.Mesa.Nome.ToLower().Contains(busca.ToLower()) ||
                        pedido.ProdutosPedidos.Any(produto =>
                            produto.NomeCliente.ToLower().Contains(busca.ToLower()) ||
                            produto.Receita.Nome.ToLower().Contains(busca.ToLower())
                        )
                    )
                )
            ).Count();
        }
    }
}
