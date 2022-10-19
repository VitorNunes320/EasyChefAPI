using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("produtos_pedidos")]
    public class ProdutoPedido
    {
        [Column("receita_id")]
        public Guid ReceitaId { get; set; }

        [Column("quantidade")]
        public decimal Quantidade { get; set; }

        [Column("nome_cliente")]
        public string NomeCliente { get; set; }

        [Column("valor")]
        public decimal Valor { get; set; }

        [Column("metodo_pagamento")]
        public int MetodoPagamento { get; set; }

        [Column("pedido_id")]
        public Guid PedidoId { get; set; }
    }
}
