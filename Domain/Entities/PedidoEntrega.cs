using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("pedidos_entregas")]
    public class PedidoEntrega
    {
        public PedidoEntrega() : base() { }

        [Column("rua")]
        public string Rua { get; set; }

        [Column("bairro")]
        public string Bairro { get; set; }

        [Column("cidade")]
        public string Cidade { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        [Column("pais")]
        public string Pais { get; set; }
    }
}
