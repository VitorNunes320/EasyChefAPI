using Domain.Enums;

namespace Domain.Models.Pedido
{
    public class PedidoModel
    {
        /// <summary>
        /// Id do pedido
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Código do pedido
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Data do pedido
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Valor total do pedido
        /// </summary>
        public decimal ValorTotal { get; set; }

        /// <summary>
        /// Quantidade de itens do pedido
        /// </summary>
        public decimal QuantidadeItens { get; set; }

        /// <summary>
        /// Observações do pedido
        /// </summary>
        public string Observacoes { get; set; }

        /// <summary>
        /// Produtos do pedido
        /// </summary>
        public List<ProdutoPedidoModel> Produtos { get; set; }

        /// <summary>
        /// Nome do usuário que fez o pedido
        /// </summary>
        public string UsuarioNome { get; set; }

        /// <summary>
        /// Nome do vendedor
        /// </summary>
        public string VendedorNome { get; set; }

        /// <summary>
        /// Id do vendedor
        /// </summary>
        public Guid VendedorId { get; set; }

        /// <summary>
        /// Status do pedido
        /// </summary>
        public StatusPedido Status { get; set; }

        /// <summary>
        /// Tipo do pedido
        /// </summary>
        public TipoPedido TipoPedido { get; set; }

        /// <summary>
        /// Id da mesa
        /// </summary>
        public Guid MesaId { get; set; }
    }
}
