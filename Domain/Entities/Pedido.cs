using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("pedido")]
    public class Pedido : EntidadeBase
    {
        public Pedido() : base() { }

        [Column("codigo")]
        public int Codigo { get; set; }

        [Column("valor_total")]
        public decimal ValorTotal { get; set; }

        [Column("quantidade_itens")]
        public int QuantidadeItens { get; set; }

        [Column("observacoes")]
        public string Observacoes { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("vendedor_id")]
        public Guid VendedorId { get; set; }

        public Usuario Vendedor { get; set; }

        [Column("empresa_id")]
        public Guid EmpresaId { get; set; }

        public Empresa Empresa { get; set; }

        [Column("tipo_pedido")]
        public int TipoPedido { get; set; }

        [Column("mesa_id")]
        public Guid MesaId { get; set; }

        public Mesa Mesa { get; set; }

        [Column("pedido_entrega_id")]
        public Guid PedidoEntregaId { get; set; }

        public PedidoEntrega PedidoEntrega { get; set; }

        public List<ProdutoPedido> ProdutosPedidos { get; set; }
    }
}
