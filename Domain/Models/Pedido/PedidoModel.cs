namespace Domain.Models.Pedido
{
    public class PedidoModel
    {

        public Guid Id { get; set; }

        public string Codigo { get; set; }

        public DateTime Data { get; set; }

        public decimal ValorTotal { get; set; }

        public decimal QuantidadeItens { get; set; }

        public string Observacoes { get; set; }

        public List<ProdutoPedidoModel> Produtos { get; set; }

        public string UsuarioNome { get; set; }

        public string UsuarioId { get; set; }

        public string VendedorNome { get; set; }

        public string VendedorId { get; set; }

        public int Status { get; set; }
    }
}
